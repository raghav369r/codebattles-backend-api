namespace CodeBattles_Backend.Domain.ValueObjects;

public class ErrorJson
{
  public string ErrorType { get; set; } = null!;
  public string ErrorMessage { get; set; } = null!;
  public int FailedTestCase { get; set; }
  public string? TCInput { get; set; }
  public string? ExpectedOutput { get; set; }
  public string? ActualOutput { get; set; }

}