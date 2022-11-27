using Journal.Models;
using Microsoft.EntityFrameworkCore;

namespace Journal.Repositories
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<WorkLog> WorkLogs { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }
    }
}
