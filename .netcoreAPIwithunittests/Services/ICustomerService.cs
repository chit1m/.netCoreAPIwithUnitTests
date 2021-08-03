using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteCustomers.Entities;
using static WebsiteCustomers.Constants;

namespace WebsiteCustomers.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomers(CategoryEnum customerCategoryName);
    }
}