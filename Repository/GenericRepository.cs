using Coursework.Contracts;
using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Repository
{
    public class GenericRepository<T> : RepositoryBase<T>, IGenericRepository<T> where T : class
    {
        public GenericRepository(CourseworkEntities context)
            : base(context)
        {
        }

        public void CreateGeneric(T entity) =>
            Create(entity);

        public void DeleteGeneric(T entity) =>
            Delete(entity);

        public IEnumerable<T> FindAllGeneric(bool trackChanges) =>
            FindAll(trackChanges)
            .ToList();

        public void UpdateGeneric(T entity) =>
            Update(entity);

        IEnumerable<T> IGenericRepository<T>.FindByConditionGeneric(Expression<Func<T, bool>> expression, bool trackChanges) =>
            FindByCondition(expression, trackChanges)
            .ToList();
    }
}
