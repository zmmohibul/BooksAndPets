using System.Text;
using API.Dtos.BookDtoAggregate.AuthorDtos;
using API.Dtos.BookDtoAggregate.BookDtos;
using API.Dtos.BookDtoAggregate.LanguageDtos;
using API.Dtos.BookDtoAggregate.PublisherDtos;
using API.Dtos.ProductDtoAggregate.CategoryDtos;
using API.Dtos.ProductDtoAggregate.DepartmentDtos;
using API.Dtos.ProductDtoAggregate.ProductDtos;
using API.Interfaces.RepositoryInterfaces;
using API.Utils;
using API.Entities.BookAggregate;
using API.Entities.ProductAggregate;
using API.Utils.QueryParameters;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class BookRepository : IBookRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public BookRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<BookDtoList>> GetAllBookDtos(BookQueryParameters bookQueryParameters)
    {
        var bookQueryable = _context.Books.AsQueryable();

        if (bookQueryParameters.CategoryId != null)
        {
            bookQueryable = bookQueryable.Where(book => book.Product.Categories.Any(pc => pc.Id == bookQueryParameters.CategoryId));
        }

        if (bookQueryParameters.MinPrice != null)
        {
            bookQueryable = bookQueryable.Where(book =>
                book.Product.PriceList.Select(p => p.UnitPrice).Min() >= bookQueryParameters.MinPrice);
        }
        
        if (bookQueryParameters.MaxPrice != null)
        {
            bookQueryable = bookQueryable.Where(book =>
                book.Product.PriceList.Select(p => p.UnitPrice).Max() <= bookQueryParameters.MaxPrice);
        }

        if (bookQueryParameters.AuthorIds != null)
        {
            bookQueryable = bookQueryable.Where(book =>
                book.Authors.Select(ba => ba.Id).Intersect(bookQueryParameters.AuthorIds).Any());
        }
        
        if (bookQueryParameters.PublisherIds != null)
        {
            bookQueryable = bookQueryable.Where(book => bookQueryParameters.PublisherIds.Contains(book.PublisherId));
        }
        
        if (bookQueryParameters.LanguageIds != null)
        {
            bookQueryable = bookQueryable.Where(book => bookQueryParameters.LanguageIds.Contains(book.LanguageId));
        }
        
        if (!string.IsNullOrEmpty(bookQueryParameters.SearchTerm))
        {
            bookQueryParameters.SearchTerm = bookQueryParameters.SearchTerm.ToLower();
            bookQueryable = bookQueryable.Where(book => 
                book.Product.Name.Contains(bookQueryParameters.SearchTerm)
                || book.Product.Categories.Any(category => category.Name.Contains(bookQueryParameters.SearchTerm))
                || book.Authors.All(author => author.Name.Contains(bookQueryParameters.SearchTerm))
                || book.Publisher.Name.Contains(bookQueryParameters.SearchTerm)
                || book.Language.Name.Contains(bookQueryParameters.SearchTerm));
        }
        
        var authors = await bookQueryable
            .Select(book => book.Authors)
            .SelectMany(author => author)
            .Distinct()
            .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        var publishers = await bookQueryable
            .Select(book => book.Publisher)
            .Distinct()
            .ProjectTo<PublisherDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        bookQueryable = bookQueryParameters.OrderByOption switch
        {
            OrderByOption.NameAsc => bookQueryable.OrderBy(book => book.Product.Name),
            OrderByOption.NameDesc => bookQueryable.OrderByDescending(book => book.Product.Name),
            OrderByOption.PriceAsc => bookQueryable.OrderBy(book => book.Product.PriceList.Min(p => p.UnitPrice)),
            OrderByOption.PriceDesc => bookQueryable.OrderByDescending(book => book.Product.PriceList.Min(p => p.UnitPrice)),
            _ => bookQueryable.OrderBy(book => book.Product.Name)
        };

        var books = await PaginatedList<BookDto>
            .CreatePaginatedListAsync(bookQueryable.ProjectTo<BookDto>(_mapper.ConfigurationProvider),
                bookQueryParameters.PageNumber, bookQueryParameters.PageSize);
        
        var bookList = new BookDtoList(books, authors, publishers);
        
        return new Result<BookDtoList>(200, bookList);
    }

    public async Task<Result<BookDto>> GetBookDtoById(int id)
    {
        var book = await _context.Books
            .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(book => book.Id == id);
        
        return book == null
            ? new Result<BookDto>(404, $"Book id:{id} not found.")
            : new Result<BookDto>(200, book);
    }

    public async Task<Result<BookDto>> CreateBook(CreateBookDto createBookDto)
    {
        var department = await _context.ProductDepartments
            .FirstOrDefaultAsync(pd => pd.Id == createBookDto.DepartmentId);
        if (department == null)
        {
            return new Result<BookDto>(400, $"Department id: {createBookDto.DepartmentId} does not exist");
        }

        var categories = await _context.ProductCategories
            .Where(pc => createBookDto.CategoryIds.Contains(pc.Id))
            .ToListAsync();
        foreach (var category in categories.Where(category => category.DepartmentId != createBookDto.DepartmentId))
        {
            return new Result<BookDto>(400,
                $"Category id: {category.Id} does not belong to Department id: {createBookDto.DepartmentId}");
        }

        var categoryIds = categories.Select(category => category.Id).ToHashSet();
        var notFoundCategoryIds = new StringBuilder();
        foreach (var id in createBookDto.CategoryIds)
        {
            if (!categoryIds.Contains(id))
            {
                notFoundCategoryIds.Append($"{id}, ");
            }
        }

        if (notFoundCategoryIds.Length > 0)
        {
            notFoundCategoryIds.Remove(notFoundCategoryIds.Length - 2, 2);
            return new Result<BookDto>(400, $"Category id: {notFoundCategoryIds} does not exist");
        }

        var priceList = new List<Price>();
        foreach (var price in createBookDto.PriceList)
        {
            var measureType = await _context.ProductMeasureTypes.FirstOrDefaultAsync(mt => mt.Id == price.MeasureTypeId);
            if (measureType == null)
            {
                return new Result<BookDto>(400, $"Measure Type id: {price.MeasureTypeId} does not exist.");
            }

            var measureOption =
                await _context.ProductMeasureOptions.FirstOrDefaultAsync(pmo => pmo.Id == price.MeasureOptionId);
            if (measureOption == null)
            {
                return new Result<BookDto>(400, $"Measure Option id: {price.MeasureOptionId} does not exist.");
            }

            if (measureOption.MeasureTypeId != price.MeasureTypeId)
            {
                return new Result<BookDto>(400, $"Measure Option id: {price.MeasureOptionId} does not belong to Measure Type id: {price.MeasureTypeId}");
            }
            
            priceList.Add(new Price()
            {
                UnitPrice = price.UnitPrice,
                MeasureType = measureType,
                MeasureOption = measureOption,
                QuantityInStock = price.QuantityInStock
            });
        }
        
        var product = new Product
        {
            Name = createBookDto.Name,
            Description = createBookDto.Description,
            Department = department,
            Categories = categories,
            PriceList = priceList
        };

        var publisher = await _context.Publishers.FirstOrDefaultAsync(pub => pub.Id == createBookDto.PublisherId);
        if (publisher == null)
        {
            return new Result<BookDto>(400, $"Publisher id: {createBookDto.PublisherId} does not exist.");
        }

        var language = await _context.Languages.FirstOrDefaultAsync(lang => lang.Id == createBookDto.LanguageId);
        if (language == null)
        {
            return new Result<BookDto>(400, $"Language id: {createBookDto.LanguageId} does not exist.");
        }

        var authors = await _context.Authors
            .Where(author => createBookDto.AuthorIds.Contains(author.Id))
            .ToListAsync();

        var authIds = authors.Select(a => a.Id).ToHashSet();
        var notFoundAuthorIds = new StringBuilder();
        foreach (var id in createBookDto.AuthorIds)
        {
            if (!authIds.Contains(id))
            {
                notFoundAuthorIds.Append($"{id}, ");
            }
        }

        if (notFoundAuthorIds.Length > 0)
        {
            notFoundAuthorIds.Remove(notFoundAuthorIds.Length - 2, 2);
            return new Result<BookDto>(400, $"Author id: {notFoundAuthorIds} does not exist");
        }
        
        var book = new Book
        {
            Product = product,
            HighlightText = createBookDto.HighlightText,
            Publisher = publisher,
            Language = language,
            Authors = authors,
            PageCount = createBookDto.PageCount,
            PublicationDate = createBookDto.PublicationDate,
            ISBN = createBookDto.ISBN
        };

        _context.Books.Add(book);
        if (await _context.SaveChangesAsync() <= 0)
            return new Result<BookDto>(400, "Failed to create book. Try again later.");
        
        var bookDto = new BookDto
        {
            Id = book.Id,
            Name = book.Product.Name,
            Description = book.Product.Description,
            Department = _mapper.Map<DepartmentDto>(department),
            Categories = _mapper.Map<ICollection<CategoryDetailsDto>>(categories),
            PriceList = _mapper.Map<ICollection<PriceDto>>(priceList),
            Pictures = new List<ProductPictureDto>(),
            HighlightText = book.HighlightText,
            Publisher = _mapper.Map<PublisherDto>(publisher),
            Authors = _mapper.Map<ICollection<AuthorDto>>(authors),
            Language = _mapper.Map<LanguageDto>(language),
            PageCount = book.PageCount,
            PublicationDate = book.PublicationDate,
            ISBN = book.ISBN
        };

        return new Result<BookDto>(201, bookDto);
    }

    public async Task<Result<BookDto>> UpdateBook(int id, UpdateBookDto updateBookDto)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<bool>> DeleteBook(int id)
    {
        throw new NotImplementedException();
    }
}