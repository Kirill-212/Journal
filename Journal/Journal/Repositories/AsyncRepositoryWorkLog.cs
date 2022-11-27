using Journal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Journal.Repositories
{
    public class AsyncRepositoryWorkLog : AsyncRepository<WorkLog>, IAsyncRepositoryWorkLog<WorkLog>
    {
        public AsyncRepositoryWorkLog(DataBaseContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<WorkLog>> Get()
        {
            return await _dbContext.WorkLogs
                .Include(i => i.Person.Department)
                .ToListAsync();
        }

        public async Task<List<WorkLog>> GetllByDepartment(string name)
        {
            return await _dbContext.WorkLogs
                 .Include(i => i.Person.Department)
                 .Where(i => i.Person.Department.Name == name)
                 .ToListAsync();
        }
    }
}