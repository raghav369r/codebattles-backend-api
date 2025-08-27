using CodeBattles_Backend.Domain.Entities;
using CodeBattles_Backend.DTOs.AddProblemDTOs;
using CodeBattles_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBattles_Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProblemController : ControllerBase
{
  private readonly ProblemService _problemService;
  public ProblemController(ProblemService problemService)
  {
    _problemService = problemService;
  }


  [HttpPost("add")]
  public async Task<ActionResult> AddProblem([FromBody] AddProblemDTO addProblemDTO)
  {
    bool isAvailable = await _problemService.IsTitleAvailable(addProblemDTO.Title);
    if (!isAvailable) return BadRequest(new { message = "A problem already exist with provided title!!" });

    bool areTopicIdsValid = await _problemService.AreTopicIdValid(addProblemDTO.Topics);
    if (!areTopicIdsValid) return BadRequest(new { message = "One or more TopicIds are invalid!!" });

    await _problemService.AddProblemAndTCS(addProblemDTO);

    return Ok("Problem Added successfully");
  }

}