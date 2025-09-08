using CodeBattles_Backend.Domain;
using CodeBattles_Backend.Domain.Entities;
using CodeBattles_Backend.Domain.Entities.Problem;
using CodeBattles_Backend.DTOs.AddProblemDTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CodeBattles_Backend.Services.AdminService;

public class ProblemService
{
  private readonly AppDBContext _appDBContext;
  public ProblemService(AppDBContext appDBContext)
  {
    _appDBContext = appDBContext;
  }

  public async Task<bool> IsTitleAvailable(string title)
  {
    return !await _appDBContext.Problems
                  .AsNoTracking()
                  .AnyAsync(p => p.Title == title);
  }

  public async Task<bool> IsProblemIdValid(int Id)
  {
    return await _appDBContext.Problems
                  .AsNoTracking()
                  .AnyAsync(p => p.Id == Id);
  }

  public async Task<bool> IsLanguageIdValid(int Id)
  {
    return await _appDBContext.Languages
                  .AsNoTracking()
                  .AnyAsync(l => l.Id == Id);
  }

  public async Task<bool> IsProblemCodeExist(int problmId, int languageId)
  {
    return await _appDBContext.ProblemCodes
                  .AsNoTracking()
                  .AnyAsync(pc => pc.ProblemId == problmId && pc.LanguageId == languageId);
  }

  public async Task<ProblemCode?> GetProblemCodes(int id)
  {
    return await _appDBContext.ProblemCodes.FindAsync(id);
  }

  public async Task<ProblemCode?> GetProblemCodes(int problemId, int languageId)
  {
    return await _appDBContext.ProblemCodes
                  .AsNoTracking()
                  .SingleOrDefaultAsync(t =>
                        t.ProblemId == problemId
                          && t.LanguageId == languageId
                  );
  }

  public async Task<bool> UpdateProblemCode(int id, UpdateCodesDTO updateCodesDTO)
  {
    // Is problemCode id Valid
    var existingCode = await GetProblemCodes(id);
    if (existingCode == null) return false;
    // does current user has access to modify this
    if (updateCodesDTO.SolutionCode != null) existingCode.SolutionCode = updateCodesDTO.SolutionCode;
    if (updateCodesDTO.ValidationCode != null) existingCode.ValidationCode = updateCodesDTO.ValidationCode;
    if (updateCodesDTO.StartCode != null) existingCode.StartCode = updateCodesDTO.StartCode;
    await _appDBContext.SaveChangesAsync();
    return true;
  }

  public async Task<bool> DeleteProblemCode(int id)
  {
    var problemCode = await _appDBContext.ProblemCodes.FindAsync(id);
    if (problemCode == null) return false;
    _appDBContext.ProblemCodes.Remove(problemCode);
    await _appDBContext.SaveChangesAsync();
    return true;
  }

  public async Task<Language?> GetLanguage(int languageId)
  {
    return await _appDBContext.Languages.FindAsync(languageId);
  }

  public async Task<bool> AddProblemAndTCS(AddProblemDTO addProblemDTO)
  {
    IDbContextTransaction transaction = await _appDBContext.Database.BeginTransactionAsync();
    try
    {
      var problem = addProblemDTO.ToProblemEntity();
      problem.TestCases = addProblemDTO.TestCases.Select(tc => new TestCase { Input = tc.Input, Output = tc.Output }).ToList();
      problem.ExamapleTestCases = addProblemDTO.ExampletestCases.Select(tc => new ExmapleTestCase { Input = tc.Input, Output = tc.Output, Explanation = tc.Explanation }).ToList(); ;

      var addedProblem = await _appDBContext.Problems.AddAsync(problem);
      await _appDBContext.SaveChangesAsync();

      await AddTopics(problem.Id, addProblemDTO.Topics);
      await _appDBContext.SaveChangesAsync();

      await transaction.CommitAsync();

      return true;
    }
    catch (Exception ex)
    {
      transaction.Rollback();
      Console.WriteLine(ex.Message);
      return false;
    }

    // var addedProblem = await AddProblem(addProblemDTO);
    // if (addedProblem == null) return null;
    // await Task.WhenAll(
    //             AddTestCases(addProblemDTO.GetTestCases(addedProblem.Id)),
    //             AddExampleTestCases(addProblemDTO.GetExmapleTestCases(addedProblem.Id)));
    // await _appDBContext.SaveChangesAsync();
    // return addedProblem;
  }

  public async Task<bool> AreTopicIdValid(List<int> ids)
  {
    var res = await _appDBContext.Topics
                    .AsNoTracking()
                    .Where(t => ids.Contains(t.Id))
                    .Select(t => t.Id)
                    .ToListAsync();
    return res.Count == ids.Count;
  }

  public async Task<List<ExmapleTestCase>> GetExmapleTestCases(int problemId)
  {
    var exTestCases = await _appDBContext.ExampleTestCases
                            .AsNoTracking()
                            .Where(t => t.ProblemId == problemId)
                            .ToListAsync();
    return exTestCases;
  }

  public async Task<bool> AddProblemCodes(ProblemCodeDTO problemCodesDto)
  {
    await _appDBContext.ProblemCodes.AddAsync(problemCodesDto.ToProblemCodeEntity());
    await _appDBContext.SaveChangesAsync();
    return true;
  }

  private async Task AddTopics(int problemId, List<int> topicIds)
  {
    var topics = topicIds.Select(t => new ProblemTopic { TopicId = t, ProblemId = problemId });
    await _appDBContext.ProblemTopics.AddRangeAsync(topics);
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