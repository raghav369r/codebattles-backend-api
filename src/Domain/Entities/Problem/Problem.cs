using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeBattles_Backend.Domain.Enums;

namespace CodeBattles_Backend.Domain.Entities.Problem;

[Table("problems")]
public class Problem
{
  [Key]
  public int Id { get; set; }
  [Required]
  public string Title { get; set; } = null!;
  [Required]
  public string Description { get; set; } = null!;
  [Required, MaxLength(20)]
  public Difficulty Difficulty { get; set; }
  public int OrganisationId { get; set; } = 0;
  public int CreatedBy { get; set; } = 5;
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }

  public List<TestCase> TestCases { get; set; } = new List<TestCase>();
  public List<ExmapleTestCase> ExamapleTestCases { get; set; } = new List<ExmapleTestCase>();
  public List<Topic> Topics { get; set; } = new List<Topic>();
}