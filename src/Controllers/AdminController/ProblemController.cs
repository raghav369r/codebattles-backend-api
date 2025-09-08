using CodeBattles_Backend.DTOs.AddProblemDTOs;
using CodeBattles_Backend.Services.AdminService;
using Microsoft.AspNetCore.Mvc;

namespace CodeBattles_Backend.Controllers.AdminController;

[ApiController]
[Route("api/admin/[controller]/add")]
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
    // need multiple DBContext to run parellal queries
    // var problmeValidTask = _problemService.IsProblemIdValid(problemCodeDTO.ProblemId);
    // var languageValidTask = _problemService.IsLanguageIdValid(problemCodeDTO.LanguageId);
    // var codeExistTask = _problemService.IsProblemCodeExist(problemCodeDTO.ProblemId, problemCodeDTO.LanguageId);

    // await Task.WhenAll(problmeValidTask, languageValidTask, codeExistTask);

    // var (isLanValid, isProblemIdValid, isProblemCodeExist) =
    //       (await problmeValidTask, await languageValidTask, await codeExistTask);

    // List<string> errorMessage = [];
    // if (!isLanValid) errorMessage.Add("Invalid Language Id!!");
    // if (!isProblemIdValid) errorMessage.Add("Invalid Problem Id!!");
    // if (isProblemCodeExist) errorMessage.Add("Problem Codes for this language already exist for given problem!!");

    // if (errorMessage.Count != 0)
    //   return BadRequest(new { message = errorMessage });

    if (!await _problemService.IsProblemIdValid(problemCodeDTO.ProblemId))
      return BadRequest(new { message = "Invalid Problem Id!!" });
    if (!await _problemService.IsLanguageIdValid(problemCodeDTO.LanguageId))
      return BadRequest(new { message = "Invalid Language Id!!" });
    if (await _problemService.IsProblemCodeExist(problemCodeDTO.ProblemId, problemCodeDTO.LanguageId))
      return BadRequest(new { message = "Problem Codes for this language already exist for given problem" });
    var res = await _problemService.AddProblemCodes(problemCodeDTO);
    return Ok(true);
  }

  [HttpPut("codes/{id}")]
  public async Task<ActionResult> UpdateProblemCodes([FromBody] UpdateCodesDTO updateCodesDTO, int id)
  {
    var res = await _problemService.UpdateProblemCode(id, updateCodesDTO);
    if (!res) return NotFound(new { message = "ProblemCodes with given ID not found!!" });
    return Ok();
  }

  [HttpDelete("codes/{id}")]
  public async Task<ActionResult> DeleteProblemCodes(int id)
  {
    var res = await _problemService.DeleteProblemCode(id);
    if (!res) return NotFound(new { message = "ProblemCodes with given ID not found!!" });
    return Ok();
  }
}