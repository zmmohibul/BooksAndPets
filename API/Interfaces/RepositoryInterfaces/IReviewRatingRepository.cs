using API.Dtos.ProductDtoAggregate.ReviewRatingDtos;
using API.Utils;
using API.Utils.QueryParameters;

namespace API.Interfaces.RepositoryInterfaces;

public interface IReviewRatingRepository
{
    public Task<Result<PaginatedList<ReviewRatingDto>>> GetAllReviews(int productId, QueryParameter queryParameters);
    public Task<Result<ReviewRatingDto>> CreateRating(string currentUserName, CreateReviewRatingDto createReviewRatingDto);
    public Task<Result<ReviewRatingDto>> UpdateRating(int id, string currentUserName, CreateReviewRatingDto createReviewRatingDto);
}