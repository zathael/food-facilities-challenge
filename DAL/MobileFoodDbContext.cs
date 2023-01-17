using CsvHelper;
using FoodFacilities.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FoodFacilities.Api.DAL
{
    /// <summary>
    /// DBContext for MobileFoodFacilities
    /// </summary>
    public class MobileFoodDbContext : DbContext
    {
        public DbSet<MobileFoodFacility> mobileFoodFacilities { get; set; }

        /// <summary>
        /// Loads the MobileFoodFacility data from the CSV file
        /// dev note: overrides OnModelCreating to use an in-memory database
        /// dev note 2: remember to use usings!
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            using (var reader = new StreamReader("DAL\\MobileFoodFacility.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<MobileFoodFacility>();
                    modelBuilder.Entity<MobileFoodFacility>().HasData(records);
                }
            }
        }
    }
}
