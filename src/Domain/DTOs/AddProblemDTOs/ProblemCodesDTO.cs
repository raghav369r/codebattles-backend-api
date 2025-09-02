using System.ComponentModel.DataAnnotations;
using CodeBattles_Backend.Domain.Entities.Problem;

namespace CodeBattles_Backend.DTOs.AddProblemDTOs;

public class ProblemCodeDTO
{
  [Required]
  [Range(1, 1e9, ErrorMessage = "Inalid ProblemId")]
  public int ProblemId { get; set; }
  [Required]
  [Range(1, 12, ErrorMessage = "Inalid LanguageId")]
  public int LanguageId { get; set; }
  [Required]
  public string StartCode { get; set; } = null!;
  [Required]
  public string ValidationCode { get; set; } = null!;
  public string? SolutionCode { get; set; } = null!;
  public ProblemCode ToProblemCodeEntity()
  {
    return new ProblemCode
    {
      ProblemId = ProblemId,
      LanguageId = LanguageId,
      StartCode = StartCode,
      ValidationCode = ValidationCode,
      SolutionCode = SolutionCode ?? "",
    };
  }
}