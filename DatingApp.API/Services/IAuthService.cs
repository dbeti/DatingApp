using System.Threading.Tasks;
using DatingApp.API.DTOs;
using DatingApp.API.Models;

namespace DatingApp.API.Services
{
    public interface IAuthService
    {
        Task<UserDTO> Register(UserDTO userDTO);

        Task<UserDTO> Login(UserDTO userDTO);

        Task<bool> UserExists(string userName);
    }
}