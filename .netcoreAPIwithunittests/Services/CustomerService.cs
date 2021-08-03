using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebsiteCustomers.Entities;
using static WebsiteCustomers.Constants;

namespace WebsiteCustomers.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly IConfiguration _configuration;
        public CustomerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<Customer>> GetCustomers(CategoryEnum customerCategoryName)
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("SQLDatabase")))
            {
                string custName = customerCategoryName.GetType()
                        .GetMember(customerCategoryName.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>().Name;
                var sql = "SELECT * FROM [Website].[Customers] where CustomerCategoryName='" + custName + "'";
                connection.Open();
                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var employee = new Customer()
                    {
                        CustomerName = reader["CustomerName"].ToString(),
                        CityName = reader["CityName"].ToString(),
                        PrimaryContact = reader["PrimaryContact"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString()
                    };
                    customers.Add(employee);
                }
            }
            return customers;
        }
    }
}
