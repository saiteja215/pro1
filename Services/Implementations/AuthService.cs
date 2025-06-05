using EcommerceBackend.Data;
using EcommerceBackend.Models.DTOs;
using EcommerceBackend.Models;
using EcommerceBackend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EcommerceBackend.Helpers;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly JwtHelper _jwtHelper;
    private readonly PasswordHasher _hasher;

    public AuthService(ApplicationDbContext context, JwtHelper jwtHelper, PasswordHasher hasher)
    {
        _context = context;
        _jwtHelper = jwtHelper;
        _hasher = hasher;
    }

    public async Task<AuthResponseDto> RegisterAsync(UserDto dto, string password)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email)) return null;

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = _hasher.HashPassword(password),
            Role = dto.Role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            Token = _jwtHelper.GenerateJwtToken(user),
            User = new UserDto { Id = user.Id, Email = user.Email, FullName = user.FullName, Role = user.Role }
        };
    }

    public async Task<AuthResponseDto> LoginAsync(AuthDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || !_hasher.VerifyPassword(user.PasswordHash, dto.Password)) return null;

        return new AuthResponseDto
        {
            Token = _jwtHelper.GenerateJwtToken(user),
            User = new UserDto { Id = user.Id, Email = user.Email, FullName = user.FullName, Role = user.Role }
        };
    }

    public string GenerateJwtToken(UserDto user)
    {
        return _jwtHelper.GenerateJwtToken(new User
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role
        });
    }
}
