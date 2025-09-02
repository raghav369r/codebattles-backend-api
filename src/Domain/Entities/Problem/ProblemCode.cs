using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBattles_Backend.Domain.Entities.Problem;

[Table("problemCodes")]
public class ProblemCode
{
  [Key]
  public int Id { get; set; }
  [Required]
  public int ProblemId { get; set; }
  [Required]
  public int LanguageId { get; set; }
  [Required]
  public string StartCode { get; set; } = null!;
  [Required]
  public string ValidationCode { get; set; } = null!;
  [Required]
  public string SolutionCode { get; set; } = null!;

}