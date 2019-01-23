using AutoMapper;
using DatingApp.API.Models;
using DatingApp.API.Repositories;

namespace DatingApp.API.Services
{
    public class UserService : CrudService<User>, IUserService
    {
        public UserService(IGenericRepository<User> genericRepository, IMapper mapper) : base(genericRepository, mapper)
        {
        }
    }
}