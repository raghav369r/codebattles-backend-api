using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBattles_Backend.Domain.Entities.Problem;

[Table("testCases")]
public class TestCase
{
  [Key]
  public int Id { get; set; }

  [Required]
  public int ProblemId { get; set; }

  [Required]
  public string Input { get; set; } = null!;

  [Required]
  public string Output { get; set; } = null!;

}