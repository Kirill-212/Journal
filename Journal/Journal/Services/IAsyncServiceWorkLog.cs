using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Journal.Services
{
    public interface IAsyncServiceWorkLog<T, D, V> : IAsyncService<T, D>
    {
        Task<List<V>> GetDataChart();

        Task<List<V>> GetDepartmentDataChart();

        Task<List<V>> GetDataChart(DateTime? start,DateTime? end);

        Task<List<V>> GetDepartmentDataChart(DateTime? start, DateTime? end);
    }
}
