using System.Collections.Generic;
using System.Threading.Tasks;

namespace Journal.Repositories
{
    public interface IAsyncRepositoryWorkLog<T>:IAsyncRepository<T>
    {
        Task<List<T>> GetllByDepartment(string name);
    }
}
