using EmployeeAppAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAppAPI.Data
{
    public class EmployeeAppDbContext : DbContext
    {
        public EmployeeAppDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Employee> Employee { get; set; }
    }
}
