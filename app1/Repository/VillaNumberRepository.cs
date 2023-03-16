using app1.Model;
using app1.Repository.RepositoryInterface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace app1.Repository
{
    public class VillaNumberRepository : GeneralRepository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _context;
        public VillaNumberRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }


       
        public async Task<VillaNumber> Update(VillaNumber entity)
        {
            entity.Update = DateTime.Now;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
