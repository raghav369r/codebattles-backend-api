using CodeBattles_Backend.DTOs.AddProblemDTOs;
using CodeBattles_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeBattles_Backend.Controllers;

[ApiController]
[Route("api/[controller]/add")]
// [Authorize]
public class ProblemController : ControllerBase
{
  private readonly ProblemService _problemService;
  public ProblemController(ProblemService problemService)
  {
    _problemService = problemService;
  }

  [HttpPost]
  public async Task<ActionResult> AddProblem([FromBody] AddProblemDTO addProblemDTO)
  {
    bool isAvailable = await _problemService.IsTitleAvailable(addProblemDTO.Title);
    if (!isAvailable) return BadRequest(new { message = "A problem already exist with provided title!!" });

    bool areTopicIdsValid = await _problemService.AreTopicIdValid(addProblemDTO.Topics);
    if (!areTopicIdsValid) return BadRequest(new { message = "One or more TopicIds are invalid!!" });

    if (await _problemService.AddProblemAndTCS(addProblemDTO))
      return Ok("Problem Added successfully");
    return StatusCode(500, new { message = "some thing went wrong!!" });
  }

  [HttpPost("codes")]
  public async Task<ActionResult> AddProblemCodes([FromBody] ProblemCodeDTO problemCodeDTO)
  {
    if (!await _problemService.IsProblemIdValid(problemCodeDTO.ProblemId))
      return BadRequest(new { message = "Invalid Problem Id!!" });
    if (!await _problemService.IsLanguageIdValid(problemCodeDTO.LanguageId))
      return BadRequest(new { message = "Invalid Language Id!!" });
    var res = await _problemService.AddProblemCodes(problemCodeDTO);
    return Ok(true);
  }

}