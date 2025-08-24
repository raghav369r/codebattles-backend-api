using System.ComponentModel.DataAnnotations;
using CodeBattles_Backend.Domain.Entities;

namespace CodeBattles_Backend.DTOs.AddProblemDTOs;

public record ExampleTestCaseDTO
{
  [Required]
  public string Input { get; set; } = null!;

  [Required]
  public string Output { get; set; } = null!;

  public string? Explanation { get; set; }

  public ExmapleTestCase ToExampleTestCaseEntity(int problemId)
  {
    return new ExmapleTestCase
    {
      ProblemId = problemId,
      Input = this.Input,
      Output = this.Output,
      Explanation = this.Explanation
    };
  }
}