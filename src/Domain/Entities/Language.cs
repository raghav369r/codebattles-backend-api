using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBattles_Backend.Domain.Entities;

[Table("languages")]
public class Language
{
  [Key]
  public int Id { get; set; }
  [Required]
  public string Name { get; set; } = null!;
  [Required]
  public string Extension { get; set; } = null!;
  [Required]
  public bool IsCompiled { get; set; }
}