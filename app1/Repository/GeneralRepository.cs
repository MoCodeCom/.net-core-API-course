using app1.Model;
using app1.Repository.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace app1.Repository
{
    public class GeneralRepository<T>:IGeneralRepository<T>where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;
        public GeneralRepository(ApplicationDbContext context)
        {
            _context = context;
            this._dbSet = _context.Set<T>();
        }
        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await Save();
        }

        public async Task<List<T>> GetAllNotNull(Expression<Func<T, bool>>? filter = null)
        {
            //To implement query filter in 
            IQueryable<T> query = _dbSet;

            //to implement filtering if not null
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }


        public async Task<T> GetVillaNotNull(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;
            //to implement tracking
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            //to implement filtering if not null
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }



        public async Task Remove(T entity)
        {
            _dbSet.Remove(entity);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
