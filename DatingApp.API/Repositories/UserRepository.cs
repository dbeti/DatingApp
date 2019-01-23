using DatingApp.API.Data;
using DatingApp.API.Models;

namespace DatingApp.API.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository 
    {
        public UserRepository(DataContext context) : base(context) {}
    }

}