using AspNetCoreMvcJwtApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreMvcJwtApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
