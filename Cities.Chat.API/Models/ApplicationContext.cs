using Microsoft.EntityFrameworkCore;

namespace Cities.Chat.API.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:citydb01.database.windows.net,1433;Initial Catalog=CityDB;Persist Security Info=False;User ID=admindb;Password=912973kjdaLj1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<City>().HasData(
        //            new City { Id = 1, CityName = "Брест", CountryName = "Беларусь", FlagId = "33" },
        //            new City { Id = 2, CityName = "Киев", CountryName = "Украина", FlagId = "123" },
        //            new City { Id = 3, CityName = "Таллин", CountryName = "Эстония", FlagId = "12" }
        //    );
        //}
    }
}
