using PixelHub.Domain.Commons;
using PixelHub.Domain.Enums;

namespace PixelHub.Domain.Entities;

public class User : Auditable
{
    public string Firstname { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public bool Email_Confirmed { get; set; }

    public UserRole UserRole { get; set; }

    public string PasswordHash { get; set; } = string.Empty;
}
