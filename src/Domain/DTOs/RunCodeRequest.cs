namespace CodeBattles_Backend.DTOs;

public record RunCodeRequest
{
  public string Code { set; get; }
  public string Language { set; get; }
  public string Input { set; get; }
}