using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Repositories;

namespace DatingApp.API.Services
{
    public class CrudService<T> : ICrudService<T> where T: class
    {
        private readonly IGenericRepository<T> _genericRepository;
        private readonly IMapper _mapper;

        public CrudService(IGenericRepository<T> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TMapper>> Get<TMapper>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes) 
        {
            var data = await _genericRepository.FilterAsync(predicate, 
                x => x, 
                includes);

            var dataToReturn = _mapper.Map<IEnumerable<TMapper>>(data);

            return dataToReturn;
        }
    }
}