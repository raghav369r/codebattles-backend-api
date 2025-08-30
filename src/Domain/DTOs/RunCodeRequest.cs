using System.ComponentModel.DataAnnotations;

namespace CodeBattles_Backend.DTOs;

public record RunCodeRequest
{
  [Required]
  public string Code { set; get; } = null!;
  [Required]
  public string Language { set; get; } = null!;
  public string? Input { set; get; }
  public int ProblemID { get; set; } = 0;
}