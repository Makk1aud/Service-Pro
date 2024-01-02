using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> FindAllGeneric(bool trackChanges);
        IEnumerable<T> FindByConditionGeneric(Expression<Func<T, bool>> expression, bool trackChanges);
        void CreateGeneric(T entity);
        void UpdateGeneric(T entity);
        void DeleteGeneric(T entity);
    }
}
