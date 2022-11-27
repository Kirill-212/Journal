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
    public class AsyncServicePerson : IAsyncService<Person, GetPersonDto>
    {
        private readonly IAsyncRepository<Person> _asyncRepository;

        public AsyncServicePerson(IAsyncRepository<Person> asyncRepository)
        {
            _asyncRepository = asyncRepository;
        }

        public async Task Create(Person Person)
        {
            await _asyncRepository.Create(Person);
        }

        public async Task Delete(int id)
        {
            Person Person = await _asyncRepository.GetById(id);
            if (Person != null)
            {
                await _asyncRepository.Delete(Person);
            }
        }

        public async Task<PagedList<GetPersonDto>> GetAll(IMapper mapper, QueryStringPagingParameters queryStringPagingParameters)
        {
            IEnumerable<Person> Persons = await _asyncRepository.Get();
            List<GetPersonDto> PersonDtos = mapper
                .Map<List<GetPersonDto>>(
                Persons.OrderBy(i => i.LastName).ToList()
                );
            int count = PersonDtos.Count();
            PersonDtos = PersonDtos
                .Skip((queryStringPagingParameters.PageNumber - 1) * queryStringPagingParameters.PageSize)
                .Take(queryStringPagingParameters.PageSize).ToList();

            return PagedList<GetPersonDto>.ToPagedList(count, PersonDtos,
                    queryStringPagingParameters.PageNumber,
                   queryStringPagingParameters.PageSize);
        }

        public async Task<List<Person>> GetAll()
        {
            return (await _asyncRepository.Get()).ToList();
        }

        public async Task<Person> GetById(int id)
        {
            return await _asyncRepository.GetById(id);
        }

        public async Task Update(Person person)
        {
            Person per = await _asyncRepository.GetById(person.Id);
            if (per != null)
            {
                per.FirstName = person.FirstName;
                per.LastName = person.LastName;
                per.Surname = person.Surname;
                per.DepartmentId = person.DepartmentId;
                await _asyncRepository.Update(per);
            }
        }
    }
}

