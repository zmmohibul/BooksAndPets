using System.Security.Claims;
using API.Data;
using API.Dtos.ProductDtoAggregate.ReviewRatingDtos;
using API.Interfaces.RepositoryInterfaces;
using API.Utils.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/products/{productId}/[controller]")]
public class ReviewRatingController : BaseApiController
{
    private readonly IReviewRatingRepository _reviewRatingRepository;
    private readonly DataContext _context;

    public ReviewRatingController(IReviewRatingRepository reviewRatingRepository, DataContext context)
    {
        _reviewRatingRepository = reviewRatingRepository;
        _context = context;
    }
    
    [HttpGet("ratings")]
    public async Task<IActionResult> GetAllRatings(int productId)
    {
        var ratings = await _context.ReviewRatings
            .Where(r => r.ProductId == productId)
            .Select(r => r.Rating)
            .GroupBy(r => r)
            .Select(g => new { Rating = g.Key, Count = g.Count() })
            .ToListAsync();

        return Ok(ratings);
    }

    [HttpGet("reviews")]
    public async Task<IActionResult> GetAllReviews(int productId, [FromQuery] QueryParameter queryParameter)
    {
        return HandleResult(await _reviewRatingRepository.GetAllReviews(productId, queryParameter));
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> CreateReviewRating(CreateReviewRatingDto createReviewRatingDto)
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        var result = await _reviewRatingRepository.CreateRating(userName, createReviewRatingDto);
        return result.StatusCode == 200 ? Ok(result.Data) : HandleResult(result);
    }

    [Authorize(Roles = "User")]
    [HttpPut("{reviewRatingId}")]
    public async Task<IActionResult> UpdateReviewRating(int reviewRatingId, CreateReviewRatingDto createReviewRatingDto)
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        return HandleResult(
            await _reviewRatingRepository.UpdateRating(reviewRatingId, userName, createReviewRatingDto));
    }
}