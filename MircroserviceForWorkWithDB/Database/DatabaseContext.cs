using Microsoft.EntityFrameworkCore;
using MircroserviceForWorkWithDB.Database.Entities;

namespace MircroserviceForWorkWithDB.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<City> City { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=DESKTOP-KC6P8SG\MSSQLSERVER1; 
                initial catalog=WeatherForecastMicroservice;persist security info=True;
                user=Weather;password=weather");
        }
    }
}
