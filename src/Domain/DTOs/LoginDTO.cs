using System.ComponentModel.DataAnnotations;

namespace CodeBattles_Backend.DTOs;

public record LoginDTO
{
  [Required]
  [EmailAddress]
  public string Email { set; get; } = "";
  [Required]
  [MinLength(5, ErrorMessage = "Password must be of at least length 5")]
  public string Password { set; get; } = "";
  public Dictionary<string, string>? Validate()
  {
    Dictionary<string, string> errors = [];
  
    return errors;
  }
}