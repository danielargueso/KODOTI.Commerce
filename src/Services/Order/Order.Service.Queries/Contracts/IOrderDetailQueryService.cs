using Order.Service.Queries.DTOs;
using Service.Common.Collection;

namespace Order.Service.Queries.Contracts
{
    public interface IOrderDetailQueryService
	{
		public Task<DataCollection<OrderDetailDto>> GetAllAsync(int page, int take, IEnumerable<int>? ids = null);
		public Task<DataCollection<OrderDetailDto>> GetByOrderIdAsync(int orderId, int page, int take, IEnumerable<int>? ids = null);
		public Task<OrderDetailDto> GetAsync(int id);
	}
}

