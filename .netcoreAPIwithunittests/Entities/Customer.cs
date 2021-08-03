using System.Diagnostics.CodeAnalysis;

namespace WebsiteCustomers.Entities
{
    //It isn't required to add unit unit tests for property getters and setters. Hence [ExcludeFromCodeCoverage] attribute is added so that this class is not considered while finding the code coverage of tests
    [ExcludeFromCodeCoverage]
    public class Customer
    {
        public string CustomerName { get; set; }
        public string PrimaryContact { get; set; }
        public string PhoneNumber { get; set; }
        public string CityName { get; set; }
    }
}
