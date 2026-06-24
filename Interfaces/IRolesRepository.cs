using api_gestion_ecole.Dtos.Roles;
using Microsoft.AspNetCore.Identity;

namespace api_gestion_ecole.Interfaces
{
    public interface IRolesRepository
    {
        public Task<List<IdentityRole>> GetAllAsync();
        public Task<IdentityRole?> GetByIdAsync(string id);
        public Task<IdentityRole?> CreateAsync(CreateRoleDto createPeriodeDto);
        public Task<IdentityRole?> UpdateAsync(string id, UpdateRoleDto updateRoleDto);
        public Task<IdentityRole?> DeleteAsync(string roleName);
        public Task<bool> IsRoleExistAsync(string roleName);
    }
}