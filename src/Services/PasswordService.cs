using CodeBattles_Backend.Domain.DTOs;
using Microsoft.AspNetCore.Identity;

namespace CodeBattles_Backend.Services;

public class PasswordService
{
    private readonly IPasswordHasher<UserJwtDTO> _passwordHasher;

    public PasswordService(IPasswordHasher<UserJwtDTO> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string HashPassword(UserJwtDTO user, string plainPassword)
    {
        return _passwordHasher.HashPassword(user, plainPassword);
    }

    public bool ComparePassword(UserJwtDTO? user, string hashedPassword, string plainPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(user!, hashedPassword, plainPassword);
        return result == PasswordVerificationResult.Success;
    }
}
