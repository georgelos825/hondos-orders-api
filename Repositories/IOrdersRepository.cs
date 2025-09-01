using HondosOrders.Api.Models;

namespace HondosOrders.Api.Repositories;

public interface IOrdersRepository
{
    Task<IEnumerable<OrderDto>> FindByInvDateAsync(DateTime date, bool useExerciseTable);
}
