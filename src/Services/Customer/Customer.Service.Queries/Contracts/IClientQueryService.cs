using Customer.Service.Queries.DTOs;
using Service.Common.Collection;

namespace Customer.Service.Queries.Contracts
{
    public interface IClientQueryService
	{
        public Task<DataCollection<ClientDTO>> GetAllAsync(int page, int take, IEnumerable<int>? clients = null);
        public Task<ClientDTO> GetAsync(int id);
	}
}

