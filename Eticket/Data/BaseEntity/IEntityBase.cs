using System.Linq.Expressions;

namespace Eticket.Data.BaseEntity
{
    public interface IEntityBase<T> where T:class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(params Expression<Func<T, Object>>[]includeProperties);
        Task<T> GetById(int id);

       Task<int> AddAsync(T entity);
       int Update(int id, T entity);
       Task<int> DeleteById(int id);
    }
}
