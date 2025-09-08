using CodeBattles_Backend.Domain;
using CodeBattles_Backend.Domain.Entities.Problem;
using CodeBattles_Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeBattles_Backend.Services.UserService;

public class ProblemService
{
  private readonly AppDBContext _appDBContext;
  public ProblemService(AppDBContext appDBContext)
  {
    _appDBContext = appDBContext;
  }

  public async Task<List<ProblemDTO>> GetAllProblems()
  {
    return await _appDBContext.Problems
                  .Where(t => true)
                  .Select(t => new ProblemDTO
                  {
                    Id = t.Id,
                    Title = t.Title,
                    Difficulty = t.Difficulty.ToString(),
                    CreatedAt = t.CreatedAt
                  })
                  .ToListAsync();
  }

  public async Task<ProblemResponseDTO?> GetProblem(int id)
  {
    var problem = await _appDBContext.Problems.FindAsync(id);
    if (problem == null) return null;
    return ProblemResponseDTO.GetDTO(problem);
  }

}