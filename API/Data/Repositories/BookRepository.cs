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
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public BookRepository(DataContext context, IProductRepository productRepository, IMapper mapper)
    {
        _context = context;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<BookDtoList>> GetAllBookDtos(BookQueryParameters bookQueryParameters)
    {
        var bookQueryable = _productRepository.GetFilteredQueryable<Book>(bookQueryParameters);

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
                book.Authors.All(author => author.Name.Contains(bookQueryParameters.SearchTerm))
                || book.Product.Name.Contains(bookQueryParameters.SearchTerm)
                || book.Product.Categories.All(category => category.Name.Contains(bookQueryParameters.SearchTerm))
                || book.Publisher.Name.Contains(bookQueryParameters.SearchTerm));
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
        
        var books = await PaginatedList<BookDto>
            .CreatePaginatedListAsync(bookQueryable.ProjectTo<BookDto>(_mapper.ConfigurationProvider),
                bookQueryParameters.PageNumber, bookQueryParameters.PageSize);

        return new Result<BookDtoList>(200, new BookDtoList(books, authors, publishers));
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
        var productCreationResult = await _productRepository.CreateProduct(createBookDto);
        if (productCreationResult.Data == null)
        {
            return new Result<BookDto>(productCreationResult.StatusCode, productCreationResult.Message);
        }
        
        var book = _mapper.Map<Book>(createBookDto);
        book.Product = productCreationResult.Data;

        var valResult = await ValidateIdsInCreateBookDto(book, createBookDto);
        if (valResult.StatusCode != 200)
        {
            return new Result<BookDto>(valResult.StatusCode, valResult.Message);
        }

        _context.Books.Add(book);
        return await _context.SaveChangesAsync() <= 0 
            ? new Result<BookDto>(400, "Failed to create book. Try again later.") 
            : new Result<BookDto>(201, _mapper.Map<BookDto>(book));
    }

    public async Task<Result<BookDto>> UpdateBook(int id, CreateBookDto createBookDto)
    {
        var book = await _context.Books.FirstOrDefaultAsync(book => book.Id == id);
        if (book == null)
        {
            return new Result<BookDto>(404, "Book not found.");
        }

        var prodValResult = await _productRepository.ValidateIdsInCreateProductDto(book.Product, createBookDto);
        if (prodValResult.StatusCode != 200)
        {
            return new Result<BookDto>(prodValResult.StatusCode, prodValResult.Message);
        }

        var bookValResult = await ValidateIdsInCreateBookDto(book, createBookDto);
        if (bookValResult.StatusCode != 200)
        {
            return new Result<BookDto>(bookValResult.StatusCode, bookValResult.Message);
        }

        _mapper.Map(createBookDto, book);

        await _context.SaveChangesAsync();
        return new Result<BookDto>(200, _mapper.Map<BookDto>(book));
    }

    public async Task<Result<bool>> DeleteBook(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(book => book.Id == id);
        if (book == null)
        {
            return new Result<bool>(404, "Book not found.");
        }

        _context.Books.Remove(book);
        return await _context.SaveChangesAsync() > 0
            ? new Result<bool>(204)
            : new Result<bool>(400, "Failed to delete book. Try again later.");
    }

    private async Task<Result<Book>> ValidateIdsInCreateBookDto(Book book, CreateBookDto createBookDto)
    {
        var publisher = await _context.Publishers.FirstOrDefaultAsync(pub => pub.Id == createBookDto.PublisherId);
        if (publisher == null)
        {
            return new Result<Book>(400, $"Publisher id: {createBookDto.PublisherId} does not exist.");
        }
        
        var language = await _context.Languages.FirstOrDefaultAsync(lang => lang.Id == createBookDto.LanguageId);
        if (language == null)
        {
            return new Result<Book>(400, $"Language id: {createBookDto.LanguageId} does not exist.");
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
            return new Result<Book>(400, $"Author id: {notFoundAuthorIds} does not exist");
        }

        book.Publisher = publisher;
        book.Language = language;
        book.Authors = authors;

        return new Result<Book>(200, book);
    }
}