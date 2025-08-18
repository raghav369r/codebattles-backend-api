namespace CodeBattles_Backend.Domain.DTOs;

public record UserJwtDTO
{
  public int Id { set; get; }
  public string UserName { set; get; } = "";
  public string FirstName { set; get; } = "";
  public string LastName { set; get; } = "";
  public string Role { set; get; } = "";
  public string Email { set; get; } = "";
}
