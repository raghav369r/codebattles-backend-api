using System.ComponentModel.DataAnnotations;
using CodeBattles_Backend.Domain.Entities;

namespace CodeBattles_Backend.DTOs;

public class RegisterDTO
{

  [Required]
  [MinLength(3, ErrorMessage = "First Name must be of at least length 3")]
  public string FirstName { set; get; } = "";
  [Required]
  [MinLength(3, ErrorMessage = "Last Name must be of at least length 3")]
  public string LastName { set; get; } = "";
  [Required]
  [EmailAddress]
  public string Email { set; get; } = "";
  [Required]
  [MinLength(5, ErrorMessage = "Password must be of at least length 5")]
  public string Password { set; get; } = "";
  bool IsCreater { set; get; } = false;
  public User ToUser()
  {
    return new User
    {
      FirstName = this.FirstName,
      LastName = this.LastName,
      Email = this.Email,
      Password = this.Password,
      IsCreater = this.IsCreater
    };
  }
  public Dictionary<string, string>? Validate()
  {
    Dictionary<string, string> errors = [];

    return errors;
  }
}