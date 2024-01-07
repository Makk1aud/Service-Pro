using Coursework.Contracts;
using Coursework.DataApp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Coursework.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly CourseworkEntities _context;

        public RepositoryBase(CourseworkEntities context)
        {
            _context = context;
        }

        public void Create(T entity) => 
            _context.Set<T>().Add(entity);


        public void Delete(T entity) =>
            _context.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges) =>
            trackChanges
            ? _context.Set<T>()
            : _context.Set<T>().AsNoTracking();


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            trackChanges
            ? _context.Set<T>().Where(expression)
            : _context.Set<T>().Where(expression).AsNoTracking();

        public void Update(T entity) =>
            _context.Set<T>().AddOrUpdate(entity);
    }
}
