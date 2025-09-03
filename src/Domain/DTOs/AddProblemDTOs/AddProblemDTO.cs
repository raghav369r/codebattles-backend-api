using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CodeBattles_Backend.Domain.Entities.Problem;
using CodeBattles_Backend.Domain.Enums;

namespace CodeBattles_Backend.DTOs.AddProblemDTOs;

public class AddProblemDTO
{
  [Required]
  public string Title { get; set; } = null!;
  [Required]
  public string Description { get; set; } = null!;
  [Required]
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public Difficulty? Difficulty { get; set; }
  [Required]
  [MinLength(1)]
  public List<TestCaseDTO> TestCases { get; set; } = null!;
  [Required]
  [MinLength(1)]
  public List<ExampleTestCaseDTO> ExampletestCases { get; set; } = null!;
  [Required]
  [MinLength(1)]
  [MaxLength(10)]
  public List<int> Topics { get; set; } = null!;

  public Problem ToProblemEntity()
  {
    if (Difficulty == null)
      throw new Exception("BadRequest");
    return new Problem
    {
      Title = this.Title,
      Description = this.Description,
      Difficulty = this.Difficulty ?? 0,
    };
  }
  public List<TestCase> GetTestCases(int problemId)
  {
    return TestCases.Select(tc => tc.ToTestCaseEntity(problemId)).ToList();
  }
  public List<ExmapleTestCase> GetExmapleTestCases(int problemId)
  {
    return ExampletestCases.Select(etc => etc.ToExampleTestCaseEntity(problemId)).ToList();
  }
  public List<int> GetTopicIds()
  {
    return [.. Topics];
  }
}