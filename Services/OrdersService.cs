using HondosOrders.Api.Models;
using HondosOrders.Api.Repositories;

namespace HondosOrders.Api.Services;

public sealed class OrdersService : IOrdersService
{
    private readonly IOrdersRepository _repo;
    private readonly IConfiguration _cfg;
    public OrdersService(IOrdersRepository repo, IConfiguration cfg)
    { _repo = repo; _cfg = cfg; }

    public async Task<OrdersResponse> GetByDateAsync(string date)
    {
        if (string.IsNullOrWhiteSpace(date))
            throw new ArgumentException("Παράμετρος 'date' υποχρεωτική (YYYY-MM-DD).");

        if (!DateTime.TryParse(date, out var dt))
            throw new ArgumentException("Άκυρη ημερομηνία. Χρησιμοποίησε YYYY-MM-DD.");

        var useExercise = _cfg.GetValue<bool>("UseExerciseTable");
        var rows = await _repo.FindByInvDateAsync(dt, useExercise);
        return new OrdersResponse { data = rows };
    }
}
