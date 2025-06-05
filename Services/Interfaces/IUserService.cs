﻿using EcommerceBackend.Models.DTOs;

namespace EcommerceBackend.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> UpdateAsync(UserDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
