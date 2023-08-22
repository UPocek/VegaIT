using System;
using Microsoft.EntityFrameworkCore;

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
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AppDb");
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Category> Categories { get; set; } = null!;
    }
}

