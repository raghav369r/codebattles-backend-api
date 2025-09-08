using CodeBattles_Backend.Domain.Entities;

namespace CodeBattles_Backend.DTOs;

public class RunCodeResponse
{
  public string stdout { get; set; }
  public string stderr { get; set; }
  public string error { get; set; }
  public Submission ToSubmissionEntity()
  {
    return new Submission
    {
      IsAccepted = string.IsNullOrEmpty(error) && string.IsNullOrEmpty(stderr),
    };
  }
}