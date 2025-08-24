using CodeBattles_Backend.Domain;
using CodeBattles_Backend.Domain.Entities;
using CodeBattles_Backend.DTOs.AddProblemDTOs;
using Microsoft.EntityFrameworkCore;

namespace CodeBattles_Backend.Services;

public class ProblemService
{
  private readonly AppDBContext _appDBContext;
  public ProblemService(AppDBContext appDBContext)
  {
    _appDBContext = appDBContext;
  }

  public async Task<bool> IsTitleAvailable(string title)
  {
    return !await _appDBContext.Problems.AnyAsync(p => p.Title == title);
  }

  public async Task AddProblemAndTCS(AddProblemDTO addProblemDTO)
  {
    var problem = addProblemDTO.ToProblemEntity();
    problem.TestCases = addProblemDTO.TestCases.Select(tc => new TestCase { Input = tc.Input, Output = tc.Output }).ToList();
    problem.ExamapleTestCases = addProblemDTO.ExampletestCases.Select(tc => new ExmapleTestCase { Input = tc.Input, Output = tc.Output, Explanation = tc.Explanation }).ToList(); ;
    var addedProblem = await _appDBContext.Problems.AddAsync(problem);
    await _appDBContext.SaveChangesAsync();

    // var addedProblem = await AddProblem(addProblemDTO);
    // if (addedProblem == null) return null;
    // await Task.WhenAll(
    //             AddTestCases(addProblemDTO.GetTestCases(addedProblem.Id)),
    //             AddExampleTestCases(addProblemDTO.GetExmapleTestCases(addedProblem.Id)));
    // await _appDBContext.SaveChangesAsync();
    // return addedProblem;
  }
  private async Task<Problem?> AddProblem(AddProblemDTO addProblemDTO)
  {
    return (await _appDBContext.Problems.AddAsync(addProblemDTO.ToProblemEntity())).Entity;
  }

  private async Task<bool> AddTestCases(IEnumerable<TestCase> testCases)
  {
    await _appDBContext.TestCases.AddRangeAsync(testCases);
    return true;
  }

  private async Task<bool> AddExampleTestCases(IEnumerable<ExmapleTestCase> exampleTestCases)
  {
    await _appDBContext.ExampleTestCases.AddRangeAsync(exampleTestCases);
    return true;
  }
}