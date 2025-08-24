using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBattles_Backend.Domain.Entities;

[Table("topics")]
public class Topic
{
  public int Id { get; set; }

  [Required, MaxLength(50)]
  public string Name { get; set; } = null!;
}