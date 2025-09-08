using CodeBattles_Backend.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace CodeBattles_Backend.Controllers.UserController;

[ApiController]
[Route("api/user/[Controller]")]
public class ProblemController : ControllerBase
{
  private readonly ProblemService _problemService;
  public ProblemController(ProblemService problemService)
  {
    _problemService = problemService;
  }

  [HttpGet]
  public async Task<ActionResult> GetProblems()
  {
    var problems = await _problemService.GetAllProblems();
    return Ok(problems);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult> GetProblem(int id)
  {
    var problem = await _problemService.GetProblem(id);
    if (problem == null) return NotFound(new { message = "No Problem withh given Id" });
    return Ok(problem);
  }

}