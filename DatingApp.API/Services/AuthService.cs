using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Repositories;
using DatingApp.API.Models;
using System;

namespace DatingApp.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<User> Login(string userName, string password)
        {
            var users = await _authRepository.FilterAsync(x => x.UserName == userName,
                x => x);
            
            var user = users.FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            var passwordCorrect = VerifyPassword(password, user.PasswordHash, user.PasswordSalt);

            if (!passwordCorrect)
            {
                return null;
            }

            return user;
        }


        public async Task<User> Register(string userName, string password)
        {
            var user = new User()
            {
                UserName = userName
            };

            (var passwordHash, var passwordSalt) = EncryptPassword(password);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _authRepository.CreateAsync(user);

            return user;
        }

        public async Task<bool> UserExists(string userName)
        {
            var users = await _authRepository.FilterAsync(x => x.UserName == userName,
                x => x); 

            var user = users.FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            return true;
        }

        private (byte[] passwordHash, byte[] passwordSalt) EncryptPassword(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var passwordSalt = hmac.Key;
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return (passwordHash, passwordSalt);
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                var numberOfDifferences = computedHash.Except(passwordHash).Count();

                if (numberOfDifferences > 0)
                {
                    return false;
                }

                return true;
            }
        }
    }
}