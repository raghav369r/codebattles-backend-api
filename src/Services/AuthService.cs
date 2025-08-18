using CodeBattles_Backend.Domain;
using CodeBattles_Backend.Domain.DTOs;
using CodeBattles_Backend.Domain.Entities;
using CodeBattles_Backend.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CodeBattles_Backend.Services;

public class AuthService
{
  private readonly AppDBContext _appDBContext;
  private readonly PasswordService _passwordService;
  public AuthService(AppDBContext appDBContext, PasswordService passwordService)
  {
    _appDBContext = appDBContext;
    _passwordService = passwordService;
  }
  public async Task<User?> RegisterUser(RegisterDTO registerDTO)
  {
    if (await IsUserWithEmailExist(registerDTO.Email)) return null;
    User user = registerDTO.ToUser();
    user.Password = _passwordService.HashPassword(new UserJwtDTO(), registerDTO.Password);
    var createdUser = await _appDBContext.Users.AddAsync(user);
    await _appDBContext.SaveChangesAsync();
    return createdUser.Entity;
  }
  private async Task<bool> IsUserWithEmailExist(string email)
  {
    var exist = await _appDBContext
                        .Users
                        .AsNoTracking()
                        .SingleOrDefaultAsync(u => u.Email == email);
    return exist != null;
  }
  public async Task<User?> GetUserByEmail(string email)
  {
    var user = await _appDBContext
                        .Users
                        .AsNoTracking()
                        .SingleOrDefaultAsync(u => u.Email == email);
    return user;
  }
}


// {
//   "firstName": "fn1",
//   "lastName": "fn2",
//   "email": "email-1@gmail.com",
//   "password": "password-1"
// }