using System.ComponentModel.DataAnnotations;
using CodeBattles_Backend.Domain.Entities.Problem;

namespace CodeBattles_Backend.DTOs.AddProblemDTOs;

public class UpdateCodesDTO
{
  public string? StartCode { get; set; }
  public string? ValidationCode { get; set; }
  public string? SolutionCode { get; set; }
}