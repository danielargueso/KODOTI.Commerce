using Identity.Service.Queries.DTOs;
using Service.Common.Collection;

namespace Identity.Service.Queries.Contracts;

public interface IApplicationUserRoleQueryService
{
    Task<DataCollection<ApplicationRoleDto>> GetAllAsync(int page, int take, IEnumerable<string>? ids = null);
    Task<ApplicationRoleDto> GetAsync(string id);
    Task<DataCollection<ApplicationRoleDto>> GetAllByUserIdAsync(string applicationUserId, int page, int take);
}

