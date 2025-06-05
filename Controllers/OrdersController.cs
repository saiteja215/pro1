using EcommerceBackend.Models.DTOs;
using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceBackend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetMyOrders()
        {
            return Ok(await _orderService.GetUserOrdersAsync(UserId));
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            return Ok(await _orderService.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create()
        {
            var order = await _orderService.CreateOrderAsync(UserId);
            if (order == null) return BadRequest("Cart is empty");
            return Ok(order);
        }

        [HttpPut("{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int orderId, [FromQuery] string status)
        {
            var result = await _orderService.UpdateStatusAsync(orderId, status);
            if (!result) return BadRequest();
            return NoContent();
        }
    }
}
