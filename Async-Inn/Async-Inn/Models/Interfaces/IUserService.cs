using Async_Inn.Models.api;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Register(RegisterUser data, ModelStateDictionary modelState);
        Task<UserDto> Authenticate(string username, string password);
        Task<UserDto> GetUser(ClaimsPrincipal principal);
    }
}
