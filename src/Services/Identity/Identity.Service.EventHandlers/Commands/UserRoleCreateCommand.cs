using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Identity.Service.EventHandlers.Commands;

public class UserRoleCreateCommand : INotification
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public string RoleId { get; set; } = string.Empty;
}

