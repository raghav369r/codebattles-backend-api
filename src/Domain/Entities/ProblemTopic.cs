using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBattles_Backend.Domain.Entities;

[Table("problemTopics")]
public class ProblemTopic
{
  public int ProblemId { get; set; }
  public int TopicId { get; set; }
}