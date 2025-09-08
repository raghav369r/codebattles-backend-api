namespace CodeBattles_Backend.DTOs;

public class SubmissionResponseDTO
{
  int Id { get; set; }
  int UserId { get; set; }
  int ProblemId { get; set; }
  int LanguageId { get; set; }
  string Code { get; set; } = null!;
  bool IsAccepted { get; set; }
  string? ErrorJson { get; set; }
  int ContestId { get; set; }
  DateTime CreatedAt { get; set; }
}