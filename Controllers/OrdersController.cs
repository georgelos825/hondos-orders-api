using HondosOrders.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HondosOrders.Api.Controllers;

[ApiController]
[Route("")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _svc;
    public OrdersController(IOrdersService svc) => _svc = svc;

    // GET /get_data?date=2025-07-15
    [HttpGet("get_data")]
    public async Task<IActionResult> Get([FromQuery] string? date)
    {
        try
        {
            var result = await _svc.GetByDateAsync(date ?? "");
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
