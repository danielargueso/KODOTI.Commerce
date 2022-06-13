namespace Identity.Service.Queries.DTOs;

public class ApplicationUserDto
{
    public string Id { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    //public virtual ICollection<ApplicationRoleDto> AssignedRoles { get; set; } = new List<ApplicationRoleDto>();
}