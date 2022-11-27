using AutoMapper;
using Journal.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Journal.Services
{
    public interface IAsyncService<T,D>
    {
        Task Create(T item);

        Task Delete(int id);

        Task<List<T>> GetAll();

        Task<PagedList<D>> GetAll(IMapper mapper, QueryStringPagingParameters queryStringPagingParameters);

        Task<T> GetById(int id);

        Task Update(T item);
    }
}
