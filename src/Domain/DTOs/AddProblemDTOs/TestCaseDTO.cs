using System.ComponentModel.DataAnnotations;
using CodeBattles_Backend.Domain.Entities;

namespace CodeBattles_Backend.DTOs.AddProblemDTOs;

public record TestCaseDTO
{
  [Required]
  public string Input { get; set; } = null!;

  [Required]
  public string Output { get; set; } = null!;

  public TestCase ToTestCaseEntity(int problemId)
  {
    return new TestCase
    {
      Input = this.Input,
      Output = this.Output
    };
  }
};