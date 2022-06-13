using System.Security.Cryptography.X509Certificates;
using Identity.Persistence.Database;
using Identity.Service.Queries.Contracts;
using Identity.Service.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Identity.Service.Queries;

public class ApplicationUserRoleQueryService : IApplicationUserRoleQueryService
{
	private readonly IdentityServiceDbContext _context;

    public ApplicationUserRoleQueryService(IdentityServiceDbContext context)
    {
        _context = context;
    }

    public async Task<DataCollection<ApplicationRoleDto>> GetAllAsync(int page, int take, IEnumerable<string>? ids = null)
        => (await _context.Roles
            .Where(x => ids == null || ids.Contains(x.Id))
            .GetPagedAsync(page, take)
            ).MapTo<DataCollection<ApplicationRoleDto>>();

    public async Task<ApplicationRoleDto> GetAsync(string id)
        => (await _context.Roles.SingleAsync(x => x.Id == id))
        .MapTo<ApplicationRoleDto>();

    //public async Task<DataCollection<ApplicationRoleDto>> GetAllByUserIdAsync(string applicationUserId, int page, int take)
    //    => (await _context.Roles
    //        .Where(x => x.UserRoles.Any(y => y.UserId == applicationUserId))
    //        .GetPagedAsync(page, take)
    //        ).MapTo<DataCollection<ApplicationRoleDto>>();
    public async Task<DataCollection<ApplicationRoleDto>> GetAllByUserIdAsync(string applicationUserId, int page, int take)
    {
        var assignedRolesToUser = _context.UserRoles
            .Where(x => x.UserId.Equals(applicationUserId))
            .Select(x => x.RoleId)
            .ToList();
        var roles = await _context.Roles
            .Where(x => assignedRolesToUser.Contains(x.Id))
            .GetPagedAsync(page,take);

        return roles.MapTo<DataCollection<ApplicationRoleDto>>();
    }
}

