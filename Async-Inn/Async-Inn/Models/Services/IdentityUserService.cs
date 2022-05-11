using Async_Inn.Models.api;
using Async_Inn.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class IdentityUserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;

        public IdentityUserService(UserManager<ApplicationUser> manager)
        {
            _userManager = manager;
        }

        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                //check if password is correct
                //PasswordVerificationResult result = _userManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, password);
                if (await _userManager.CheckPasswordAsync(user, password))
                {
                    UserDto userDto = new UserDto
                    {
                        Id = user.Id,
                        Username = user.UserName,
                    };
                    return userDto;
                }
            }
            return null;
        }

        public async Task<UserDto> Register(RegisterUser data, ModelStateDictionary modelstate)
        {
            var user = new ApplicationUser
            {
                UserName = data.Username,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,
                PasswordHash = data.Password
            };

            var result = await _userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                UserDto userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                };
                return userDto;
            }
            foreach (var error in result.Errors)
            {
                var errorKey =
                    // nameof will go to the RegisterUser class and take property name 
                    error.Code.Contains("Password") ? /* key name will be -> */nameof(data.Password) :
                    error.Code.Contains("Email") ? /* key name will be -> */nameof(data.Email) :
                    error.Code.Contains("UserName") ? /* key name will be -> */nameof(data.Username) :
                    "";
                modelstate.AddModelError(errorKey, error.Description);
            }
            return null;
        }
    }
}
