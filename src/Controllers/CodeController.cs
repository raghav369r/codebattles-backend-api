using CodeBattles_Backend.DTOs;
using CodeBattles_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeBattles_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
// [Authorize]
public class CodeController : ControllerBase
{
  private readonly GlotAPIService _glotAPIService;
  public CodeController(GlotAPIService glotAPIService)
  {
    _glotAPIService = glotAPIService;
  }

  [HttpPost("run")]
  public async Task<ActionResult<RunCodeResponse>> RunCode([FromBody] RunCodeRequest runCodeRequest)
  {
    var res = await _glotAPIService.RunCode(code: runCodeRequest.Code, language: runCodeRequest.Language, input: runCodeRequest.Input);
    return Ok(res);
  }
}