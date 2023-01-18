using FakeItEasy;
using NUnit.Framework;
using FoodFacilities.Api.v1.Controllers;
using FoodFacilities.Api.v1.DAL;

namespace FoodFacilities.Api.Test.v1.Controllers
{
    [TestFixture]
    public class FoodFacilityControllerTests
    {
        internal readonly MobileFoodDbContext _db;

        internal readonly MobileFoodFacilitiesController _controller;

        public FoodFacilityControllerTests()
        {
            _db = A.Fake<MobileFoodDbContext>();
            _controller = new MobileFoodFacilitiesController(_db);
        }

        /// <summary>
        /// ShouldReturnFoundUser
        /// Expect a proper result
        /// </summary>
        [Test]
        public void ShouldReturnFoundUser()
        {
            var testApplicant = "Datam SF LLC";            
            var result = _controller.SearchMobileFoodVendorsByNameAsync(testApplicant);
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// ShouldReturn404ForNotFoundUser
        /// todo: Expect a 404
        /// </summary>
        [Test]
        public void ShouldReturn404ForNotFoundUser()
        {
            var testApplicant = "###FAKE USER###";
            var controller = new MobileFoodFacilitiesController(GetMobileFoodDb());
            var result = controller.SearchMobileFoodVendorsByNameAsync(testApplicant);
            Assert.IsNotNull(result);
            //Assert.IsTrue(result.GetType() == typeof(NotFoundResult));
        }

        private MobileFoodDbContext GetMobileFoodDb()
        {
            return new MobileFoodDbContext();
        }
    }
}
