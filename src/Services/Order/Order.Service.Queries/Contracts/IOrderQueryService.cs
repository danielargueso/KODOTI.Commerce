using Order.Service.Queries.DTOs;
using Service.Common.Collection;

namespace Order.Service.Queries.Contracts
{
    public interface IOrderQueryService
	{
		public Task<DataCollection<OrderDto>> GetAllAsync(int page, int take, IEnumerable<int>? ids = null);
		public Task<OrderDto> GetAsync(int id);
	}
}

