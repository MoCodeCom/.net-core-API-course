using app1.Model;
using System.Linq.Expressions;

namespace app1.Repository.RepositoryInterface
{
    public interface IGeneralRepository<T>where T : class
    {

        //To add filtration for list for each null data entries.
        Task<List<T>> GetAllNotNull(Expression<Func<T, bool>>? filter = null);

        //To get villa data with no null entries
        Task<T> GetVillaNotNull(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task Create(T entity);
        Task Remove(T entity);
        
        Task Save();
    }
}
