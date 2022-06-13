using Identity.Service.Queries.DTOs;
using Service.Common.Collection;

namespace Identity.Service.Queries.Contracts;

public interface IApplicationUserQueryService
{
    Task<DataCollection<ApplicationUserDto>> GetAllAsync(int page, int take, IEnumerable<string>? ids = null);
    Task<ApplicationUserDto> GetAsync(string id);
}

