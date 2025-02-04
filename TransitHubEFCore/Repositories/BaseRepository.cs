using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TransitHubRepo.Interfaces;

namespace TransitHubEFCore.Repositories
{
    // Here i will make implimentation of IBaseRepo 
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public T Create(T Entity)
        {
           _context.Set<T>().Add(Entity);
           return Entity;
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> expression, string[]? includes)
        {
            IQueryable<T> query = _context.Set<T>();
            if(includes != null)
            {
                foreach(var include in includes)
                    query = query.Include(include);
            }
            return query.Where(expression).ToList();
        }

        public IEnumerable<T> FindAll(string[]? includes)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            return query.ToList();
        }

        public T FindOne(int id)
        {
            return _context.Set<T>().Find(id)!; 
        }

        public T FindOne(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().SingleOrDefault(expression)!;
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T Modifing(T entity)
        {
            _context.Update(entity);
            return entity;
        }
        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }
        public void DeleteMany(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        
    }
}
