using System.Collections.Generic;
using DatingApp.API.Models;
using DatingApp.API.Services;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach (var user in users)
            {
                (var passwordHash, var passwordSalt) = EncryptPassword("password");

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.UserName = user.UserName.ToLower();

                _context.Users.Add(user);
            }

            _context.SaveChanges();
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
    }
}