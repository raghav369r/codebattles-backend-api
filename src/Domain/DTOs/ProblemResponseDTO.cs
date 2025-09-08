using CodeBattles_Backend.Domain.Entities.Problem;

namespace CodeBattles_Backend.DTOs;

public class ProblemResponseDTO
{
  public int Id { get; set; }
  public string Title { get; set; } = null!;
  public string Description { get; set; } = null!;
  public string Difficulty { get; set; } = null!;
  public int OrganisationId { get; set; }
  public int CreatedBy { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }

  public static ProblemResponseDTO GetDTO(Problem problem)
  {
    return new ProblemResponseDTO
    {
      Id = problem.Id,
      Title = problem.Title,
      Description = problem.Description,
      Difficulty = problem.Difficulty.ToString(),
      OrganisationId = problem.OrganisationId,
      CreatedBy = problem.CreatedBy,
      CreatedAt = problem.CreatedAt,
      UpdatedAt = problem.UpdatedAt
    };
  }
}