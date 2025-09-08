namespace CodeBattles_Backend.DTOs;

public class ProblemDTO
{
  public int Id { get; set; }
  public string Title { get; set; } = null!;
  public string Difficulty { get; set; } = null!;
  public DateTime CreatedAt { get; set; }
}