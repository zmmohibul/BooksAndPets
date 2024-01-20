using System.Security.Claims;
using API.Dtos.OrderDtoAggregate;
using API.Interfaces.RepositoryInterfaces;
using API.Utils.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[Route("api/[controller]")]
public class OrdersController : BaseApiController
{
    private readonly IOrderRepository _orderRepository;

    public OrdersController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [Authorize(Roles = "User")]
    [HttpGet]
    public async Task<IActionResult> GetAllOrdersForUser([FromQuery] QueryParameter queryParameters)
    {
        var result = await _orderRepository.GetAllOrdersForUser(User.FindFirst(ClaimTypes.Name)?.Value, queryParameters);
        return HandleResult(result);
    }
    
    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
    {
        var result = await _orderRepository.CreateOrder(User.FindFirst(ClaimTypes.Name)?.Value, createOrderDto);
        if (result.StatusCode == 201)
        {
            return Ok(result.Data);
        }
        
        return HandleResult(result);
    }

    [HttpPost("cancel/{orderId}")]
    public async Task<IActionResult> CancelOrder(int orderId)
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        return HandleResult(await _orderRepository.CancelOrder(userName, orderId));
    }
}