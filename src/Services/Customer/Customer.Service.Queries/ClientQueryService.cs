using Customer.Persistance.Database;
using Customer.Service.Queries.Contracts;
using Customer.Service.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Customer.Service.Queries;

public class ClientQueryService : IClientQueryService
{
	private readonly CustomerDbContext _context;

    public ClientQueryService(CustomerDbContext context)
    {
        _context = context;
    }

    public async Task<DataCollection<ClientDTO>> GetAllAsync(int page, int take, IEnumerable<int>? clients = null)
    {
        var collection = await _context.Clients
               .Where(x => clients == null || clients.Contains(x.ClientId))
               .OrderByDescending(x => x.ClientId)
               .GetPagedAsync(page, take);

        return collection.MapTo<DataCollection<ClientDTO>>();
    }

    public async Task<ClientDTO> GetAsync(int id)
    {
        return (await _context.Clients.SingleAsync(x => x.ClientId == id))
            .MapTo<ClientDTO>();
    }

}

