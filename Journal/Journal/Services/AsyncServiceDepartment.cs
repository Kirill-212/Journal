using AutoMapper;
using Journal.Dto.Get;
using Journal.Models;
using Journal.Paging;
using Journal.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Journal.Services
{
    public class AsyncServiceDepartment : IAsyncService<Department,GetDepartmentDto>
    {
        private readonly IAsyncRepository<Department> _asyncRepository;

        public AsyncServiceDepartment(IAsyncRepository<Department> asyncRepository)
        {
            _asyncRepository = asyncRepository;
        }

        public async Task Create(Department department)
        {
            await _asyncRepository.Create(department);
        }

        public async Task Delete(int id)
        {
            Department department = await _asyncRepository.GetById(id);
            if (department != null)
            {
               await _asyncRepository.Delete(department);
            }
        }

        public async Task<PagedList<GetDepartmentDto>> GetAll(IMapper mapper, QueryStringPagingParameters queryStringPagingParameters)
        {
            IEnumerable<Department> departments = await _asyncRepository.Get();
            List<GetDepartmentDto> departmentDtos = mapper
                .Map<List<GetDepartmentDto>>(
                departments.OrderBy(i => i.Name).ToList()
                );
            int count = departmentDtos.Count();
            departmentDtos = departmentDtos
                .Skip((queryStringPagingParameters.PageNumber - 1) * queryStringPagingParameters.PageSize)
                .Take(queryStringPagingParameters.PageSize).ToList();

            return PagedList<GetDepartmentDto>.ToPagedList(count, departmentDtos,
                    queryStringPagingParameters.PageNumber,
                   queryStringPagingParameters.PageSize);
        }

        public async Task<List<Department>> GetAll()
        {
            return (await _asyncRepository.Get()).ToList();
        }

        public async Task<Department> GetById(int id)
        {
            return await _asyncRepository.GetById(id);
        }

        public async Task Update(Department department)
        {
            Department dep = await _asyncRepository.GetById(department.Id);
            if (dep != null)
            {
                dep.Name = department.Name;
                dep.ShortName = department.ShortName;
               await _asyncRepository.Update(dep);
            }
        }
    }
}
