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
        /// Applicant
        /// </summary>
        public string Applicant { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FacilityType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Cnn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LocationDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int BlockLot { get; set; }
        public string Block { get; set; }
        public string Lot { get; set; }
        public string Permit { get; set; }
        public string Status { get; set; }
        public string FoodItems { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Schedule { get; set; }
        public string Dayshours { get; set; }
        public string NOISent { get; set; }
        public string Approved { get; set; }
        public string Received { get; set; }
        public string PriorPermit { get; set; }
        public string ExpirationDate { get; set; }
        public string Location { get; set; }
        public string FirePreventionDistricts { get; set; }
        public string PoliceDistricts { get; set; }
        public string SupervisorDistricts { get; set; }
        public string ZipCodes { get; set; }
        public string NeighborhoodsOld { get; set; }
    }
}
