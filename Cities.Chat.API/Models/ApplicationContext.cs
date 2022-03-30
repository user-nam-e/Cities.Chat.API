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
            //optionsBuilder.UseSqlServer(@"Server=;Initial Catalog=;Persist Security Info=False;User ID=;Password=;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<City>().HasData(
        //            new City { Id = 1, CityName = "Брест", CountryName = "белоруссия", FlagId = "33" },
        //            new City { Id = 2, CityName = "Киев", CountryName = "Украина", FlagId = "123" },
        //            new City { Id = 3, CityName = "Таллин", CountryName = "Эстония", FlagId = "12" }
        //    );
        //}
    }
}
