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
        public int LocationId { get; set; }
        /// <summary>
        /// Applicant Name
        /// </summary>
        public string Applicant { get; set; }
        /// <summary>
        /// Facility Type (Truck, Push Cart, etc.)
        /// </summary>
        public string FacilityType { get; set; }
        /// <summary>
        /// Cnn Id
        /// </summary>
        public int Cnn { get; set; }
        /// <summary>
        /// Location's Description
        /// </summary>
        public string LocationDescription { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Blocklot Id
        /// </summary>
        public int BlockLot { get; set; }
        /// <summary>
        /// Block Id
        /// </summary>
        public int Block { get; set; }
        /// <summary>
        /// Lot Id
        /// </summary>
        public int Lot { get; set; }
        /// <summary>
        /// Permit 
        /// </summary>
        public string Permit { get; set; }
        /// <summary>
        /// Status Message
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Descriptive List of Items as a string
        /// </summary>
        public string FoodItems { get; set; }
        /// <summary>
        /// X position, sometimes null
        /// </summary>
        public int? X { get; set; }
        /// <summary>
        /// Y position, sometimes null
        /// </summary>
        public int? Y { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public int Latitude { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public int Longitude { get; set; }
        /// <summary>
        /// Schedule as a url
        /// </summary>
        public string Schedule { get; set; }
        /// <summary>
        /// Days & Hours of Vendor (non-useful format)
        /// </summary>
        public string Dayshours { get; set; }
        /// <summary>
        /// NOISent 
        /// dev note: all columns null. Could be a Boolean flag that "yes we sent a NOI" or be more complex. set as string
        /// </summary>
        public string NOISent { get; set; }
        /// <summary>
        /// Approved Time
        /// dev note: kept as string - we are given no UTC or timezone. We 'could' assume PST and convert UTC from there. 
        ///           Or we could keep data as data, and interpret it when we utilize it.
        /// </summary>
        public string Approved { get; set; }
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
        public string ExpirationDate { get; set; }
        /// <summary>
        /// Location 
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// FirePreventionDistricts Id?
        /// dev note: Could be a quantity, but is an occasionall null, singular unsigned number
        /// </summary>
        public int? FirePreventionDistricts { get; set; }
        /// <summary>
        /// PoliceDistricts Id?
        /// dev note: Could be a quantity, but is an occasionall null, singular unsigned number
        /// </summary>
        public int? PoliceDistricts { get; set; }
        /// <summary>
        /// SupervisorDistricts Id?
        /// dev note: Could be a quantity, but is an occasionall null, singular unsigned number
        /// </summary>
        public int? SupervisorDistricts { get; set; }
        /// <summary>
        /// Zipcode
        /// dev note: Why is this plural when we are expecting a single value?
        /// </summary>
        public int? ZipCodes { get; set; }
        /// <summary>
        /// NeighborhoodsOld Id
        /// 
        /// </summary>
        public int? NeighborhoodsOld { get; set; }
    }
}
