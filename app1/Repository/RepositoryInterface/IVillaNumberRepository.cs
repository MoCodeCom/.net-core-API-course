using app1.Model;
using System.Linq.Expressions;

namespace app1.Repository.RepositoryInterface
{
    public interface IVillaNumberRepository:IGeneralRepository<VillaNumber>
    {
        //Task<IEnumerable<VillaModel>> GetAll();
        //To add filtration for list for each null data entries.
        //Task<List<VillaModel>> GetAllNotNull(Expression<Func<VillaModel, bool>> filter = null);
        //Task<VillaModel> GetVilla(int id);
        //To get villa data with no null entries
        //Task<VillaModel> GetVillaNotNull(Expression<Func<VillaModel, bool>> filter = null, bool tracked = true);
        //Task Create(VillaModel entity);
        //Task Remove(VillaModel entity);
        Task<VillaNumber> Update(VillaNumber entity);
        //Task Save();
    }
}
