namespace HondosOrders.Api.Models;

public sealed class OrdersResponse
{
    public IEnumerable<OrderDto> data { get; set; } = Enumerable.Empty<OrderDto>();
}
