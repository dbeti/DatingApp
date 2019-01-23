using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DatingApp.API.Services
{
    public interface ICrudService<T> where T: class
    {
         Task<IEnumerable<TMapper>> Get<TMapper>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    }
}