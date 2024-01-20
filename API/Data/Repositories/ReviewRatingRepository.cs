using API.Dtos.ProductDtoAggregate.ReviewRatingDtos;
using API.Entities.Identity;
using API.Entities.ProductAggregate;
using API.Interfaces.RepositoryInterfaces;
using API.Utils;
using API.Utils.QueryParameters;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class ReviewRatingRepository : IReviewRatingRepository
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public ReviewRatingRepository(DataContext context, UserManager<User> userManager,  IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<ReviewRatingDto>>> GetAllReviews(int productId, QueryParameter queryParameters)
    {
        var query = _context.ReviewRatings
            .Where(r => r.ProductId == productId)
            .ProjectTo<ReviewRatingDto>(_mapper.ConfigurationProvider);
        
        var data = await PaginatedList<ReviewRatingDto>
            .CreatePaginatedListAsync(query, queryParameters.PageNumber, queryParameters.PageSize);

        return new Result<PaginatedList<ReviewRatingDto>>(200, data);
    }

    public async Task<Result<ReviewRatingDto>> CreateRating(string currentUserName, CreateReviewRatingDto createReviewRatingDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName.Equals(currentUserName));
        if (user == null)
        {
            return new Result<ReviewRatingDto>(401, "Please log in to continue");
        }

        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == createReviewRatingDto.ProductId);
        if (product == null)
        {
            return new Result<ReviewRatingDto>(404, "Product not found.");
        }

        var order = await _context.Orders
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == createReviewRatingDto.OrderId);
        if (order == null)
        {
            return new Result<ReviewRatingDto>(404, "Order does not exist.");
        }

        if (order.UserId != user.Id)
        {
            return new Result<ReviewRatingDto>(403, "This is not your order!");
        }

        var rating = await _context.ReviewRatings
            .Where(r => r.User.UserName == currentUserName && r.ProductId == product.Id && r.OrderId == order.Id)
            .FirstOrDefaultAsync();
        if (rating != null)
        {
            return new Result<ReviewRatingDto>(400, "You have already rated this product. Try updating.");
        }
        
        rating = new ReviewRating
        {
            Rating = createReviewRatingDto.Rating,
            User = user,
            Product = product,
            Order = order
        };
        if (!string.IsNullOrEmpty(createReviewRatingDto.Review))
        {
            rating.Review = createReviewRatingDto.Review;
        }
        _context.ReviewRatings.Add(rating);

        if (await _context.SaveChangesAsync() > 0)
        {
            product.AverageRating = await _context.ReviewRatings
                .Where(r => r.ProductId == product.Id)
                .AverageAsync(r => r.Rating);;
            
            product.RatingCount = await _context.ReviewRatings
                .Where(r => r.ProductId == product.Id)
                .CountAsync();

            await _context.SaveChangesAsync();
            return new Result<ReviewRatingDto>(200, _mapper.Map<ReviewRatingDto>(rating));
        }
        
        return new Result<ReviewRatingDto>(400, "Failed to rate the product. Try again later.");
    }

    public async Task<Result<ReviewRatingDto>> UpdateRating(int id, string currentUserName, CreateReviewRatingDto createReviewRatingDto)
    {
        var rating = await _context.ReviewRatings
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (rating == null)
        {
            return new Result<ReviewRatingDto>(400, "Rating does not exist.");
        }

        if (!rating.User.UserName.Equals(currentUserName))
        {
            return new Result<ReviewRatingDto>(403, "This is not your rating.");
        }
        
        rating.Rating = createReviewRatingDto.Rating;
        if (!string.IsNullOrEmpty(createReviewRatingDto.Review))
        {
            rating.Review = createReviewRatingDto.Review;
        }

        await _context.SaveChangesAsync();
        return new Result<ReviewRatingDto>(200, _mapper.Map<ReviewRatingDto>(rating));
    }
}