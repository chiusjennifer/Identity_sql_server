using Identity_sql_server.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity_sql_server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customers> customers { get; set; }
    }
}
