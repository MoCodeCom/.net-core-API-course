using app1.Model;
using app1.Repository.RepositoryInterface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace app1.Repository
{
    public class VillaRepository : GeneralRepository<VillaModel>, IVillaRepository
    {
        private readonly ApplicationDbContext _context;
        public VillaRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        //public async Task Create(VillaModel entity)
        //{
        //    await _context.Villas.AddAsync(entity);
        //    await Save();
        //}

        //public async Task<IEnumerable<VillaModel>> GetAll()
        //{
        //    return await _context.Villas.ToListAsync();
        //}

        //public async Task<List<VillaModel>> GetAllNotNull(Expression<Func<VillaModel, bool>> filter = null)
        //{
        //    //To implement query filter in 
        //    IQueryable<VillaModel> query = _context.Villas;

        //    //to implement filtering if not null
        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }
        //    return await query.ToListAsync();
        //}

        //public async Task<VillaModel> GetVilla(int id)
        //{

        //    return await _context.Villas.FirstOrDefaultAsync(u => u.Id == id);
        //}

        //public async Task<VillaModel> GetVillaNotNull(Expression<Func<VillaModel, bool>> filter = null, bool tracked = true)
        //{
        //    IQueryable<VillaModel> query = _context.Villas;
        //    //to implement tracking
        //    if (!tracked)
        //    {
        //        query = query.AsNoTracking();
        //    }
        //    //to implement filtering if not null
        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }
        //    return await query.FirstOrDefaultAsync();
        //}

        //public async Task Remove(VillaModel entity)
        //{
        //    _context.Villas.Remove(entity);
        //    await Save();
        //}

        //public async Task Save()
        //{
        //    await _context.SaveChangesAsync();
        //}

        public async Task<VillaModel> Update(VillaModel entity)
        {
            entity.UpdateData = DateTime.Now;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
