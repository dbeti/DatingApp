using System.Threading.Tasks;
using DatingApp.API.DTOs;
using DatingApp.API.Models;

namespace DatingApp.API.Services
{
    public interface IAuthService
    {
        Task<UserForRegistrationDto> Register(UserForRegistrationDto userForRegistrationDto);

        Task<UserForLoginDto> Login(UserForLoginDto userForLoginDto);

        Task<bool> UserExists(string userName);
    }
}