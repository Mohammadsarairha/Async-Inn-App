using Async_Inn.Models.api;
using Async_Inn.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class IdentityUserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;
        private JwtTokenService _tokenService;

        public IdentityUserService(UserManager<ApplicationUser> manager, JwtTokenService jwtTokenService)
        {
            _userManager = manager;
            _tokenService = jwtTokenService;
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
                        Token = await _tokenService.GetToken(user, System.TimeSpan.FromMinutes(15)),
                        Roles = await _userManager.GetRolesAsync(user)
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
                IList<string> Roles = new List<string>();
                Roles.Add("Administrator");
                await _userManager.AddToRolesAsync(user, Roles);
                UserDto userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = await _tokenService.GetToken(user, System.TimeSpan.FromMinutes(15)),
                    Roles = await _userManager.GetRolesAsync(user)
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
        public async Task<UserDto> GetUser(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName
            };
        }
    }
}
