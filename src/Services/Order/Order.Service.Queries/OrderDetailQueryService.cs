using Microsoft.EntityFrameworkCore;
using Order.Persistence.Database;
using Order.Service.Queries.Contracts;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Order.Service.Queries
{
	public class OrderDetailQueryService : IOrderDetailQueryService
    {
        private readonly OrderDbContext _context;

        public OrderDetailQueryService(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<DataCollection<OrderDetailDto>> GetAllAsync(int page, int take, IEnumerable<int>? ids = null)
        {
            var collection = await _context.OrderDetails
                   .Where(x => ids == null || ids.Contains(x.OrderDetailId))
                   .OrderByDescending(x => x.OrderDetailId)
                   .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<OrderDetailDto>>();
        }

        public async Task<DataCollection<OrderDetailDto>> GetByOrderIdAsync(int orderId, int page, int take, IEnumerable<int>? ids = null)
        {
            var collection = await _context.OrderDetails
                .Where(x => x.OrderId == orderId)
                .Where(x => ids == null || ids.Contains(x.OrderDetailId))
                .OrderByDescending(x => x.OrderDetailId)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<OrderDetailDto>>();
        }

        public async Task<OrderDetailDto> GetAsync(int id)
        {
            return (await _context.OrderDetails.SingleAsync(x => x.OrderDetailId == id))
                .MapTo<OrderDetailDto>();
        }
    }
}

