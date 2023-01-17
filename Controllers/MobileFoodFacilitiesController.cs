using FoodFacilities.Api.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodFacilities.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileFoodFacilitiesController : ControllerBase
    {
        private readonly MobileFoodDbContext _mobileFoodDbContext;

        public MobileFoodFacilitiesController(MobileFoodDbContext mobileFoodDbContext)
        {
            _mobileFoodDbContext = mobileFoodDbContext;
        }

        /// <summary>
        /// GetAllMobileFoodVendors
        /// Returns a list of MobileFoodFacility
        /// dev note: this is a simple example of a GET request
        /// dev note 2: limited to top 20. don't hammer this, we don't paginate!
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAllMobileFoodVendors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllMobileFoodVendors()
        {
            var mobileFoodVendors = await _mobileFoodDbContext.MobileFoodFacilities.ToListAsync();
            return Ok(mobileFoodVendors.Take(20));
        }

        /// <summary>
        /// SearchMobileFoodVendorsByName
        /// Returns a MobileFoodFacility
        /// uses optional param "status" to filter by status
        /// </summary>
        /// <param name="name"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("search/{name}", Name = "SearchMobileFoodVendorsByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchMobileFoodVendorsByName(string name, string status = "")
        {
            var mobileFoodVendors = await _mobileFoodDbContext.MobileFoodFacilities
               .Where(m =>
                   // compare only lowercase to lowercase names for easier searching
                   m.Applicant.ToLower().Contains(name.ToLower())).ToListAsync();
            
            // dev note: I'd rather avoid this via ternanry operator in the LINQ query, but FirstOrDefaultAsync is being tricky.
            //           I'm sure there's a cleaner better way to do this, but I'm not spending more time on this.
            if (!string.IsNullOrWhiteSpace(status))
            {
                var vendorWithStatus = mobileFoodVendors.FirstOrDefault(m =>
                    // comparing lowercase for consistency despite data format being CAPS
                    m.Status.ToLower().Contains(status.ToLower()));
                

                if (vendorWithStatus == null)
                {
                    return NotFound();
                }

                return Ok(vendorWithStatus);
            }

            var mobileFoodVendor = mobileFoodVendors.FirstOrDefault();

            if (mobileFoodVendor == null)
            {
                return NotFound();
            }

            return Ok(mobileFoodVendor);
        }

        /// <summary>
        /// SearchMobileFoodVendorsByAddress
        /// Returns a MobileFoodFacility
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpGet("street/{address}", Name = "SearchMobileFoodVendorsByAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchMobileFoodVendorsByAddress(string address)
        {
            var mobileFoodVendor = await _mobileFoodDbContext.MobileFoodFacilities
                .FirstOrDefaultAsync(m =>
                    // compare only lowercase to lowercase addresses for easier searching
                    m.Address.ToLower().Contains(address.ToLower())
                );

            if (mobileFoodVendor == null)
            {
                return NotFound();
            }

            return Ok(mobileFoodVendor);
        }

        /// <summary>
        /// SearchMobileFoodVendorsByLocation
        /// Returns 5 MobileFoodFacilities within a 0.05 by 0.06 mile area of the given lat/long
        /// dev note: this solution is not ideal, we do not rank our results by name, ID, etc.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        [HttpPost("location", Name = "SearchMobileFoodVendorsByLocation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchMobileFoodVendorsByLocation(double latitude, double longitude, string status = "APPROVED")
        {
            // search within 0.05 miles latitude 
            var latitudeVendors = await _mobileFoodDbContext.MobileFoodFacilities
                .Where(m => m.Latitude > latitude - 0.01 && m.Latitude < latitude + 0.01).ToListAsync();

            // search within 0.06 miles longitude
            var longitudeVendors = latitudeVendors.Where(m => m.Longitude > longitude - 0.01 && m.Longitude < longitude + 0.01);

            // dev note: I'd rather avoid this via ternanry operator in the LINQ query, but FirstOrDefaultAsync is being tricky.
            //           I'm sure there's a cleaner better way to do this, but I'm not spending more time on this.
            if (!string.IsNullOrWhiteSpace(status))
            {
                var vendorsWithStatus = longitudeVendors.Where(m =>
                   // comparing lowercase for consistency despite data format being CAPS
                   m.Status.ToLower().Contains(status.ToLower()));


                if (!vendorsWithStatus.Any())
                {
                    return NotFound();
                }

                return Ok(vendorsWithStatus.Take(5));
            }

            if (!longitudeVendors.Any())
            {
                return NotFound();
            }

            return Ok(longitudeVendors.Take(5)); // Take returns from the top
        }
    }
}
