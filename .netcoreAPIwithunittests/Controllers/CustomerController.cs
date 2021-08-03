using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebsiteCustomers.Entities;
using WebsiteCustomers.Services;
using static WebsiteCustomers.Constants;

namespace WebsiteCustomers.Controllers
{

    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        

        
        [ProducesResponseType(typeof(List<Customer>),(int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string),(int)HttpStatusCode.NotFound)]
        [HttpGet, Route("api/[controller]/{customerCategoryName}")]
        public async Task<IActionResult> Get([FromRoute] CategoryEnum customerCategoryName)
        {
            List<Customer> customers = await _customerService.GetCustomers(customerCategoryName);
            if(customers.Count == 0)
            {
                return new NotFoundObjectResult(Constants.NoCustomersFound);
            }
            return new OkObjectResult(customers);
        }
        
    }
}
