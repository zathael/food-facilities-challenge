using CsvHelper;
using CsvHelper.Configuration;
using FoodFacilities.Api.v1.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FoodFacilities.Api.v1.DAL
{
    /// <summary>
    /// DBContext for MobileFoodFacilities
    /// </summary>
    public class MobileFoodDbContext : DbContext
    {
        public DbSet<MobileFoodFacility> MobileFoodFacilities { get; set; }

        public MobileFoodDbContext()
        {
        }

        public MobileFoodDbContext(DbContextOptions<MobileFoodDbContext> options)
            : base(options)
        {
        }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "MobileFoodFacilities");
        }

        /// <summary>
        /// Loads the MobileFoodFacility data from the CSV file
        /// dev note: overrides OnModelCreating to use an in-memory database
        /// dev note 2: remember to use usings!
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MobileFoodFacility>().ToTable("MobileFoodFacilities");
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
            };

            var records = new List<MobileFoodFacility>();

            using (var reader = new StreamReader("DAL/v1/Mobile_Food_Facility_Permit.csv"))
            using (var csv = new CsvReader(reader, config))
                records = csv.GetRecords<MobileFoodFacility>().ToList();

            modelBuilder.Entity<MobileFoodFacility>().HasData(records);
        }
    }
}
