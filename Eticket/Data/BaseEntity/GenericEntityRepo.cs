using ETicket.data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eticket.Data.BaseEntity
{
    public class GenericEntityRepo<T> : IEntityBase<T> where T : class
    {
        private readonly EticketDbContext _context;

        public GenericEntityRepo( EticketDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
           return _context.SaveChanges();
        }

        public async Task<int> DeleteById(int id)
        {
           var result= await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(result);
            return _context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAll()
         => await _context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperties) => current.Include(includeProperties));
            return await query.ToListAsync();
        }

        public Task<IEnumerable<T>> GetAllInclude()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetById(int id)
        {
          var GetbyId=  await _context.Set<T>().FindAsync(id);
            return GetbyId;

        }

        public  int Update(int id, T entity)
        {
          var result=  _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }
    }
}
