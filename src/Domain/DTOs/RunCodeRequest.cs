using System.ComponentModel.DataAnnotations;

namespace CodeBattles_Backend.DTOs;

public record RunCodeRequest
{
  [Required]
  public string Code { set; get; } = null!;
  [Required]
  public int LanguageId { set; get; }
  public string? Input { set; get; }
  public int ProblemId { get; set; } = 0;
}