using Journal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Journal.Repositories
{
    public class AsyncRepositoryPerson : AsyncRepository<Person>, IAsyncRepository<Person>
    {
        public AsyncRepositoryPerson(DataBaseContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Person>> Get()
        {
            return await _dbContext.Persons.Include(i=>i.Department).ToListAsync();
        }
    }
}
