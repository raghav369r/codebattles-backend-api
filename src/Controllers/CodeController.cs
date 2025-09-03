using CodeBattles_Backend.Domain.Entities;
using CodeBattles_Backend.Domain.Entities.Problem;
using CodeBattles_Backend.DTOs;
using CodeBattles_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeBattles_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class CodeController : ControllerBase
{
  private readonly CodeRunService _codeRunService;
  private readonly ProblemService _problemService;
  public CodeController(CodeRunService codeRunService, ProblemService problemService)
  {
    _codeRunService = codeRunService;
    _problemService = problemService;
  }

  [HttpPost("run")]
  public async Task<ActionResult<RunCodeResponse>> RunCode([FromBody] RunCodeRequest runCodeRequest)
  {
    RunCodeResponse? response;
    int problemId = runCodeRequest.ProblemId;
    int languageId = runCodeRequest.LanguageId;
    Language? language = await _problemService.GetLanguage(languageId);

    if (language == null) return BadRequest(new { message = "Invalid Language Id!!" });
    _codeRunService.Language = language;
    if (problemId == 0) // plain code
      response = await _codeRunService.RunPlainCode(runCodeRequest);
    else
    {
      ProblemCode? problemCode = await _problemService.GetProblemCodes(problemId, languageId);
      _codeRunService.ProblemCode = problemCode;
      if (problemCode == null) return BadRequest(new { message = "Probelm and language combination are incorrect!!" });
      else // code need to run against exmaple test cases
      {
        var testCases = await _problemService.GetExmapleTestCases(problemId);
        response = await _codeRunService.RunWithETCs(runCodeRequest, testCases);
      }
    }
    return Ok(response);
  }
}