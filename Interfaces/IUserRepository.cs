using api_gestion_ecole.Dtos.Auth;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<AppUser>> GetAllAsync(QueryObjectForPeople queryObjectForPeople);
        public Task<AppUser?> GetByIdAsync(string id);
        public Task<AuthentificatedUserDto?> CreateAsync(RegistorUserDto registorUserDto);
        public Task<AuthentificatedUserDto?> UpdateAsync(string id, UpdateUserDto updateRoleDto);
        public Task<AppUser?> DeleteAsync(string roleName);
        public Task<bool> IsEmailExistAsync(string roleName);
        public Task<AuthentificatedUserDto?> LoginAsync (LoginDto loginDto);

    }
}