using System.Collections.Generic;
using System.Threading.Tasks;

namespace Journal.Repositories
{
    public interface IAsyncRepository<T>
    {
        Task<T> GetById(int id);

        Task Create(T item);

        Task<IEnumerable<T>> Get();

        Task Update(T item);

        Task Delete(T item);
    }
}
