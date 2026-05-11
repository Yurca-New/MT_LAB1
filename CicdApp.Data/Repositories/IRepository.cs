using System.Collections.Generic;
using System.Threading.Tasks;

namespace CicdApp.Data.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}