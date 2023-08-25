using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace timesheetback.Models
{
	public class TimeSheetContext : DbContext
    {
        public TimeSheetContext(DbContextOptions<TimeSheetContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build().GetSection("ConnectionStrings")["DefaultConnection"];
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<TimeEntry> TimeEntries { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Serbia" },
                new Country { Id = 2, Name = "USA" },
                new Country { Id = 3, Name = "Germany" }
            );

            modelBuilder.Entity<City>().HasData(
               new City { Id = 1, Name = "Novi Sad", Zip="21000" },
               new City { Id = 2, Name = "Chicago" , Zip = "60007" },
               new City { Id = 3, Name = "Berlin" , Zip = "10115" }
           );

            modelBuilder.Entity<Category>().HasData(
               new Category { Id = 1, Name = "Standard" },
               new Category { Id = 2, Name = "Bonus" },
               new Category { Id = 3, Name = "BugFix" }
           );

            modelBuilder.Entity<Role>().HasData(
              new Role { Id = 1, Name = "admin" },
              new Category { Id = 2, Name = "worker" }
          );

        }
    }
}

