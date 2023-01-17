using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FoodFacilities.Api.Models
{
    /// <summary>
    /// MobileFoodFacility describes a vendor, location, their food and details of their permit.
    /// dev note: chose Int over Long due to no LocationId or Cnn breaching 1/10th the magnitude of 32bit overflow
    /// </summary>
    public class MobileFoodFacility
    {
        /// <summary>
        /// LocationId
        /// </summary>
        [Name("locationid")]
        [Key] // dev note: we would check that this is a true UNIQUE
        public int LocationId { get; set; }
        /// <summary>
        /// Applicant Name
        /// </summary>
        public string? Applicant { get; set; }
        /// <summary>
        /// Facility Type (Truck, Push Cart, etc.)
        /// </summary>
        public string? FacilityType { get; set; }
        /// <summary>
        /// Cnn Id
        /// </summary>
        [Name("cnn")]
        public int Cnn { get; set; }
        /// <summary>
        /// Location's Description
        /// </summary>
        public string? LocationDescription { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Blocklot Id
        /// dev note: sometimes contains letters like an A at the end. string.
        /// </summary>
        [Name("blocklot")]
        public string? BlockLot { get; set; }
        /// <summary>
        /// Block Id
        /// dev note: sometimes this is blank! would be an int but staying as a string now
        /// </summary>
        [Name("block")]        
        public string? Block { get; set; }
        /// <summary>
        /// Lot Id
        /// dev note: sometimes contains letters like an A at the end. string.
        /// </summary>
        [Name("lot")]
        public string? Lot { get; set; }
        /// <summary>
        /// Permit 
        /// </summary>
        [Name("permit")]
        public string? Permit { get; set; }
        /// <summary>
        /// Status Message
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        /// Descriptive List of Items as a string
        /// </summary>
        public string? FoodItems { get; set; }
        /// <summary>
        /// X position, sometimes null, sometimes decimal
        /// dev note: keep as string because unclean data
        /// </summary>
        public string? X { get; set; }
        /// <summary>
        /// Y position, sometimes null
        /// dev note: keep as string because unclean data
        /// </summary>
        public string? Y { get; set; }
        /// <summary>
        /// Latitude
        /// https://www.usgs.gov/faqs/how-much-distance-does-degree-minute-and-second-cover-your-maps
        /// One degree of latitude represents ~69 miles
        /// One minute of latitude represents ~1.15 miles
        /// One second of latitude represents ~0.06 miles
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// https://www.usgs.gov/faqs/how-much-distance-does-degree-minute-and-second-cover-your-maps
        /// One degree of longitude represents ~54.6 miles
        /// One minute of longitude represents ~0.91 miles
        /// One second of longitude represents ~0.05 miles
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Schedule as a url
        /// </summary>
        public string? Schedule { get; set; }
        /// <summary>
        /// Days & Hours of Vendor (non-useful format)
        /// </summary>
        [Name("dayshours")]
        public string? Dayshours { get; set; }
        /// <summary>
        /// NOISent 
        /// dev note: all columns null. Could be a Boolean flag that "yes we sent a NOI" or be more complex. set as string
        /// </summary>
        public string? NOISent { get; set; }
        /// <summary>
        /// Approved Time
        /// dev note: kept as string - we are given no UTC or timezone. We 'could' assume PST and convert UTC from there. 
        ///           Or we could keep data as data, and interpret it when we utilize it.
        /// </summary>
        public string? Approved { get; set; }
        /// <summary>
        /// Received Id
        /// </summary>
        public int Received { get; set; }
        /// <summary>
        /// PriorPermit
        /// </summary>
        public bool PriorPermit { get; set; }
        /// <summary>
        /// Expiration Time
        /// dev note: kept as string - we are given no UTC or timezone. We 'could' assume PST and convert UTC from there. 
        ///           Or we could keep data as data, and interpret it when we utilize it.
        /// </summary>
        public string? ExpirationDate { get; set; }
        /// <summary>
        /// Location 
        /// </summary>
        public string? Location { get; set; }
        /// <summary>
        /// FirePreventionDistricts Id?
        /// dev note: Could be a quantity, but is an occasionall null, singular unsigned number
        /// </summary>
        [Name("Fire Prevention Districts")]
        public int? FirePreventionDistricts { get; set; }
        /// <summary>
        /// PoliceDistricts Id?
        /// dev note: Could be a quantity, but is an occasionall null, singular unsigned number
        /// </summary>
        [Name("Police Districts")]
        public int? PoliceDistricts { get; set; }
        /// <summary>
        /// SupervisorDistricts Id?
        /// dev note: Could be a quantity, but is an occasionall null, singular unsigned number
        /// </summary>
        [Name("Supervisor Districts")]
        public int? SupervisorDistricts { get; set; }
        /// <summary>
        /// Zipcode
        /// dev note: Why is this plural when we are expecting a single value?
        /// </summary>
        [Name("Zip Codes")]
        public int? ZipCodes { get; set; }
        /// <summary>
        /// NeighborhoodsOld Id
        /// 
        /// </summary>
        [Name("Neighborhoods (old)")]
        public int? NeighborhoodsOld { get; set; }
    }
}
