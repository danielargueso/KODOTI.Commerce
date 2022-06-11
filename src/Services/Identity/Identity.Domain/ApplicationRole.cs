using Microsoft.AspNetCore.Identity;

namespace Identity.Domain;

public class ApplicationRole : IdentityRole
{
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}

