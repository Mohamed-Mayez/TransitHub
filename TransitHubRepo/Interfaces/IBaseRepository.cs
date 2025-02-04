using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TransitHubRepo.Interfaces
{
    // here i will put every method or function segnuture  that i want to be in all Apis
    public interface IBaseRepository<T> where T : class
    {
       List<T> GetAll();
       T Create(T Entity);
       T FindOne(int id);
       T Modifing(T entity);
       T Delete(T entity);
       T FindOne(Expression<Func<T,bool>> expression);
       IEnumerable<T> FindAll(string[]? includes);
       IEnumerable<T> FindAll(Expression<Func<T,bool>> expression);
       IEnumerable<T> FindAll(Expression<Func<T,bool>> expression, string[]? includes); 
       void DeleteMany(IEnumerable<T> entities);
    }
}
