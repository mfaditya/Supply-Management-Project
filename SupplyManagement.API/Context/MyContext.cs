using Microsoft.EntityFrameworkCore;
using SupplyManagement.API.Models;

namespace SupplyManagement.API.Data
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
    }
}
