using HondosOrders.Api.Models;

namespace HondosOrders.Api.Services;

public interface IOrdersService
{
    Task<OrdersResponse> GetByDateAsync(string date);
}
