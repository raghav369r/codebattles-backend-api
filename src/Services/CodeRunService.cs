using CodeBattles_Backend.Domain.Entities;
using CodeBattles_Backend.DTOs;

namespace CodeBattles_Backend.Services;

public class CodeRunService
{
  private readonly GlotAPIService _glotAPIService;
  public CodeRunService(GlotAPIService glotAPIService)
  {
    _glotAPIService = glotAPIService;
  }
  public async Task<RunCodeResponse?> RunPlainCode(RunCodeRequest runCodeRequest)
  {
    return await _glotAPIService
                      .RunCode(
                          code: runCodeRequest.Code,
                          language: runCodeRequest.Language,
                          input: runCodeRequest.Input
                      );
  }

  public async Task<RunCodeResponse?> RunWithETCs(RunCodeRequest runCodeRequest, List<ExmapleTestCase> exmapleTestCases)
  {
    string inputString = MakeInputStringFromTCs(exmapleTestCases);
    // build code from problem
    // string code = userCode + validatorCode;
    return await _glotAPIService
                    .RunCode(
                        code: runCodeRequest.Code,
                        language: runCodeRequest.Language,
                        input: inputString
                    );
  }

  // n - no of tc's
  // for each test case
  //  id(now index) input output
  private string MakeInputStringFromTCs(List<ExmapleTestCase> exmapleTestCases)
  {
    string inputString = "";
    int tcCount = exmapleTestCases.Count;
    inputString += tcCount + "\n";
    exmapleTestCases.ForEach(etc => inputString += etc.Id + " " + etc.Input + " " + etc.Output + "\n");
    // Console.WriteLine("\n\n" + inputString);
    return inputString;
  }

}