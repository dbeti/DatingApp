using DatingApp.API.Data;
using DatingApp.API.Models;

namespace DatingApp.API.Repositories
{
    public class AuthRepository : GenericRepository<User>, IAuthRepository
    {
        public AuthRepository(DataContext context) : base(context)
        {
        }
    }
}