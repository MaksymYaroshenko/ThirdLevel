using Microsoft.EntityFrameworkCore;
using MircroserviceForWorkWithDB.Database.Entities;

namespace MircroserviceForWorkWithDB.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<City> City { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigurationManager.AppSetting["Database:ConnectionString"];
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
