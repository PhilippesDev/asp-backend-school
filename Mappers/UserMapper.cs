using api_gestion_ecole.Dtos.Auth;
using api_gestion_ecole.Models;

namespace api_gestion_ecole.Mappers
{
    public static class UserMapper
    {
        public static AppUser ToAppUserFromCreate(this RegistorUserDto registorUserDto)
        {
            return new AppUser
            {
                UserName = registorUserDto.UserName,
                Email = registorUserDto.Email
            };
        }

        public static UserDto ToUserDto(this AppUser appUser, string token = "")
        {
            return new UserDto
            {
                UserName = appUser.UserName ?? "",
                Email = appUser.Email ?? ""
            };
        }

        public static AuthentificatedUserDto ToAuthentifactedUserDto(this AppUser appUser, string token = "")
        {
            return new AuthentificatedUserDto
            {
                UserName = appUser.UserName ?? "",
                Email = appUser.Email ?? "",
                Token = token
            };
        }
    }
}