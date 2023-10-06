using PixelHub.Domain.Commons;
using PixelHub.Domain.Enums;

namespace PixelHub.Domain.Entities.User;

public class User : Human
{
    public string Email { get; set; } = string.Empty;

    public bool Email_Confirmed { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole UserRole { get; set; }
}