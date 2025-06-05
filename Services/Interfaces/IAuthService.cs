using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(UserDto dto, string password);
        Task<AuthResponseDto> LoginAsync(AuthDto dto);
        string GenerateJwtToken(UserDto user);
    }

}
