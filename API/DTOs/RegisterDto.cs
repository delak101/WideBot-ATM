using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    public long CardNum { get; set; }
    [Required]
    public int PIN { get; set; }
}
