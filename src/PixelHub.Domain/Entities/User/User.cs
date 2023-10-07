using PixelHub.Domain.Commons;
using PixelHub.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PixelHub.Domain.Entities.User;

public class User : Auditable
{
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public bool Email_Confirmed { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole UserRole { get; set; }
}