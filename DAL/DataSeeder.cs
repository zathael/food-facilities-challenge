using CsvHelper;
using FoodFacilities.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FoodFacilities.Api.DAL
{
    public class DataSeeder
    {
        public static void SeedData(MobileFoodDbContext context)
        {
            if (context.MobileFoodFacilities.Any())
            {
                return;
            }

            using (var reader = new StreamReader("DAL\\MobileFoodFacility.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<MobileFoodFacility>();
                    context.AddRange(records);
                    context.SaveChanges();
                }
            }
        }
    }
}

