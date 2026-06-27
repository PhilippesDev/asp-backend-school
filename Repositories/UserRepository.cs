using api_gestion_ecole.Data;
using api_gestion_ecole.Dtos.Auth;
using api_gestion_ecole.Helpers;
using api_gestion_ecole.Interfaces;
using api_gestion_ecole.Mappers;
using api_gestion_ecole.Models;
using api_gestion_ecole.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api_gestion_ecole.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly TokenService _tokenService;
        
        public UserRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, TokenService tokenService, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _dbContext = dbContext;
        }
        public async Task<AuthentificatedUserDto?> CreateAsync(RegistorUserDto registorUserDto)
        {
            var user = registorUserDto.ToAppUserFromCreate();
            
            var createState = await _userManager.CreateAsync(user, registorUserDto.Password);
            if(!createState.Succeeded) return null;

            var token = await _tokenService.CreateToken(user, _userManager);

            await AssignRoleToUser(user.Email ?? " ", "User");

            return user.ToAuthentifactedUserDto(token);
        }

        // public async Task<bool> IsEleveParentAsync (string telephone)
        // {
        //     var parent = await _dbContext.Parent.FirstOrDefaultAsync()
        // }

        public async Task<AppUser?> DeleteAsync(string email)
        {
            var user =  await _userManager.FindByEmailAsync(email);
            if(user == null) return null;

            var deleteState = await _userManager.DeleteAsync(user);
            if(!deleteState.Succeeded) return null;

            return user;
        }

        public async Task<List<AppUser>> GetAllAsync(QueryObjectForPeople queryObject)
        {
            var users = _userManager.Users.AsQueryable();

            if(!string.IsNullOrEmpty(queryObject.Noms))
                users = users.Where(c=>
                    c.UserName!.ToLower()
                        .Contains(queryObject.Noms.ToLower()) || 
                     c.Email!.ToLower()
                        .Contains(queryObject.Noms.ToLower())
                     );
            
            if(queryObject.IsDescending == true) 
                users = users.OrderByDescending(c=>c.Id);

            int skip = (queryObject.Page - 1) * queryObject.PageSize; 
           
            users = users.Skip(skip).Take(queryObject.PageSize);

            return await users.ToListAsync();
        }

        public async Task<AppUser?> GetByIdAsync(string id)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u=>
                                u.Id == id || u.Email == id); 
        }
        public async Task<bool> IsEmailExistAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }
        public async Task<AuthentificatedUserDto?> UpdateAsync(string id, UpdateUserDto updateUserDto)  
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id || u.Email == id);
            if (user == null) return null;
        
            if (!string.Equals(user.Email, updateUserDto.Email, StringComparison.OrdinalIgnoreCase))
            {
                var emailExiste = await _userManager.FindByEmailAsync(updateUserDto.Email);
            if (emailExiste != null) 
                {
                    throw new Exception("Cet adresse email est déjà utilisée par un autre compte.");
                }
            
                var emailResult = await _userManager.SetEmailAsync(user, updateUserDto.Email);
                if (!emailResult.Succeeded) return null;
            }

            if (!string.Equals(user.UserName, updateUserDto.UserName, StringComparison.OrdinalIgnoreCase))
            {
                var userNameExiste = await _userManager.FindByNameAsync(updateUserDto.UserName);
                if (userNameExiste != null)
                {
                    throw new Exception("Ce nom d'utilisateur est déjà pris.");
                }

                var userResult = await _userManager.SetUserNameAsync(user, updateUserDto.UserName);
                if (!userResult.Succeeded) return null;
            }


            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded) return null;

            var token = await _tokenService.CreateToken(user, _userManager);    
            return user.ToAuthentifactedUserDto(token);
        }
        public async Task<AuthentificatedUserDto?> ChangePasswordAsync(string id, ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u=>
                                u.Id == id || u.Email == id);
            
            if(user == null) return null;
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            if(!result.Succeeded) return null;


            var token = await _tokenService.CreateToken(user, _userManager);
            return user.ToAuthentifactedUserDto(token);
        }

        public async Task<AppUser?> AssignRoleToUser(string userEmail, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if(user == null) return null;
            
            var role = await _roleManager.FindByNameAsync(roleName);
            if(role == null) return null;

            var state = await _userManager.AddToRoleAsync(user, roleName);
            if(!state.Succeeded) return null;

            var roles = await _userManager.GetRolesAsync(user);

            if(roles.Count > 1)
            {
                foreach(var userRole in roles)
                {
                    if(!userRole.Equals(roleName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        await _userManager.RemoveFromRoleAsync(user, userRole);
                    }
                }
            } 

            return user;
        }

        public async Task<AuthentificatedUserDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.Users.
                        FirstOrDefaultAsync(u=>u.UserName == loginDto.UserName || u.Email == loginDto.Email);

            if(user == null) return null;

            var isPwrdCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if(!isPwrdCorrect) return null;

            var token = await _tokenService.CreateToken(user, _userManager);
            return user.ToAuthentifactedUserDto(token);
        }
    }
}