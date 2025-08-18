using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBattles_Backend.Domain.Entities;

[Table("users")]
public class User
{
  [Key]
  public int Id { get; set; }

  [Required, EmailAddress, MaxLength(100)]
  public string Email { get; set; }

  [Required, MinLength(3), MaxLength(100)]
  public string FirstName { get; set; }

  [Required, MinLength(3), MaxLength(100)]
  public string LastName { get; set; }

  [MinLength(5), MaxLength(100)]
  public string? UserName { get; set; }

  [Required, MinLength(5), MaxLength(255)]
  public string Password { get; set; }

  public bool IsCreater { get; set; } = false;

  public string? OrganisationId { get; set; }

  public DateTime? CreatedAt { get; set; }

  public DateTime? LastUpdatedAt { get; set; }

  public DateTime? LastSignIn { get; set; }

  public User() { }
}