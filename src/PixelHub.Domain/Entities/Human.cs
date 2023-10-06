using PixelHub.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace PixelHub.Domain.Entities;

public class Human : Auditable
{
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
}
