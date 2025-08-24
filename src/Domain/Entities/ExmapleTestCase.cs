using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBattles_Backend.Domain.Entities;

[Table("exampleTestCases")]
public class ExmapleTestCase
{
  [Key]
  public int Id { get; set; }

  [Required]
  public int ProblemId { get; set; }

  public string? Explanation { get; set; }

  [Required]
  public string Input { get; set; } = null!;

  [Required]
  public string Output { get; set; } = null!;

}