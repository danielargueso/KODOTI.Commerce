using Identity.Persistence.Database;
using Identity.Service.Queries.Contracts;
using Identity.Service.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Identity.Service.Queries;

public class ApplicationUserQueryService : IApplicationUserQueryService
{
	private readonly IdentityServiceDbContext _context;

    public ApplicationUserQueryService(IdentityServiceDbContext context)
    {
        _context = context;
    }

    public async Task<DataCollection<ApplicationUserDto>> GetAllAsync(int page, int take, IEnumerable<string>? ids = null)
    {
        var collection = await _context.Users
            .Where(x => ids == null || ids.Contains(x.Id))
            .GetPagedAsync(page, take);

        return collection.MapTo<DataCollection<ApplicationUserDto>>();
    }
    public async Task<ApplicationUserDto> GetAsync(string id)
        => (await _context.Users.SingleAsync(x => x.Id == id))
        .MapTo<ApplicationUserDto>();
}

