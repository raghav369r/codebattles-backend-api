using CodeBattles_Backend.Domain.Entities;
using CodeBattles_Backend.Domain.Entities.Problem;
using CodeBattles_Backend.DTOs;

namespace CodeBattles_Backend.Services;

public class CodeRunService
{
  private readonly GlotAPIService _glotAPIService;
  public Language? Language { get; set; }
  public ProblemCode? ProblemCode { get; set; }
  public CodeRunService(GlotAPIService glotAPIService)
  {
    _glotAPIService = glotAPIService;
  }
  public async Task<RunCodeResponse?> RunPlainCode(RunCodeRequest runCodeRequest)
  {
    if (Language == null) return null;
    return await _glotAPIService
                      .RunCode(
                          code: runCodeRequest.Code,
                          language: Language.Name,
                          extention: Language.Extension,
                          input: runCodeRequest.Input
                      );
  }

  public async Task<RunCodeResponse?> RunWithETCs(RunCodeRequest runCodeRequest, List<ExmapleTestCase> exmapleTestCases)
  {
    if (Language == null || ProblemCode == null) return null;
    string inputString = MakeInputStringFromTCs(exmapleTestCases);
    // build code from problem
    string code = ProblemCode.ValidationCode + runCodeRequest.Code;
    return await _glotAPIService
                    .RunCode(
                        code: code,
                        language: Language.Name,
                        extention: Language.Extension,
                        input: inputString
                    );
  }

  // n - no of tc's
  // for each test case
  //  id(now index) input output
  private static string MakeInputStringFromTCs(List<ExmapleTestCase> exmapleTestCases)
  {
    string inputString = "";
    int tcCount = exmapleTestCases.Count;
    inputString += tcCount + "\n";
    exmapleTestCases.ForEach(etc => inputString += etc.Id + " " + etc.Input + " " + etc.Output + "\n");
    return inputString;
  }

  private static string MakeInputStringFromTCs(List<TestCase> exmapleTestCases)
  {
    string inputString = "";
    int tcCount = exmapleTestCases.Count;
    inputString += tcCount + "\n";
    exmapleTestCases.ForEach(etc => inputString += etc.Id + " " + etc.Input + " " + etc.Output + "\n");
    return inputString;
  }
}