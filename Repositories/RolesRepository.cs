using api_gestion_ecole.Dtos.Roles;
using api_gestion_ecole.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class RolesRepository:IRolesRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IdentityRole?> CreateAsync(CreateRoleDto createPeriodeDto)
        {
            if(string.IsNullOrEmpty(createPeriodeDto.Name)) 
                return null;
            var roleName = createPeriodeDto.Name;
            var createState = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if(!createState.Succeeded) return null;
            return new IdentityRole{Name = roleName, NormalizedName = roleName.ToUpper()};
        }
        
        public async Task<IdentityRole?> DeleteAsync(string roleName)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r=>r.Id == roleName || r.NormalizedName == roleName.ToUpper());

            if(role == null) return null;

            var deleteState = await _roleManager.DeleteAsync(role);
            if(deleteState.Succeeded)
                return role;
            
            return null;
        }

        public async Task<bool> IsRoleExistAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<List<IdentityRole>> GetAllAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IdentityRole?> GetByIdAsync(string id)
        {
            return await _roleManager.Roles.FirstOrDefaultAsync(r=> r.Id == id  || r.NormalizedName == id.ToUpper());
        }

        public async Task<IdentityRole?> UpdateAsync(string id, UpdateRoleDto updateRoleDto)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r=>r.Id == id || r.NormalizedName == updateRoleDto.Name.ToUpper());
            if(role == null) return null;

            role.Name = updateRoleDto.Name;
            role.NormalizedName = updateRoleDto.Name.ToUpper();

            var updateState = await _roleManager.UpdateAsync(role);

            if(!updateState.Succeeded)return null;
            return role;
        }
    }
}