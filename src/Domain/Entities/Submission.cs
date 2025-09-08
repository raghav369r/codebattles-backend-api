using System.ComponentModel.DataAnnotations;

namespace CodeBattles_Backend.Domain.Entities;

public class Submission
{
  [Key]
  public int Id { get; set; }
  public int UserId { get; set; }
  public int ProblemId { get; set; }
  public int LanguageId { get; set; }
  public string Code { get; set; } = null!;
  public bool IsAccepted { get; set; }
  public string? ErrorJson { get; set; }
  public int ContestId { get; set; }
  public DateTime CreatedAt { get; set; }
}