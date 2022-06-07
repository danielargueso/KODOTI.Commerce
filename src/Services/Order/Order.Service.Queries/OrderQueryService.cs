using Microsoft.EntityFrameworkCore;
using Order.Persistence.Database;
using Order.Service.Queries.Contracts;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Order.Service.Queries;

public class OrderQueryService : IOrderQueryService
{
    private readonly OrderDbContext _context;

    public OrderQueryService(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<DataCollection<OrderDto>> GetAllAsync(int page, int take, IEnumerable<int>? ids = null)
    {
        var collection = await _context.Orders
            .Include(x => x.Items)
            .Where(x => ids == null || ids.Contains(x.OrderId))
            .OrderByDescending(x => x.OrderId)
            .GetPagedAsync(page, take);

        return collection.MapTo<DataCollection<OrderDto>>();
    }

    public async Task<OrderDto> GetAsync(int id)
    {
        return (
            await _context.Orders
            .Include(x => x.Items)
            .SingleAsync(x => x.OrderId == id)
            ).MapTo<OrderDto>();
    }
}

