using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
    public int CardNum { get; set; }
    [Required]
    public int PIN { get; set; }
}
