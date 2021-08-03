using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using WebsiteCustomers.Controllers;
using WebsiteCustomers.Entities;
using WebsiteCustomers.Services;
using Xunit;
using static WebsiteCustomers.Constants;


namespace UnitTests
{
    [ExcludeFromCodeCoverage]
    public class CustomerControllerTests
    {
        private readonly Mock<IDbCommand> _mockSqlCommand;
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly CustomerController _controller;
        public CustomerControllerTests()
        {
            _mockSqlCommand = new Mock<IDbCommand>();
            _mockCustomerService = new Mock<ICustomerService>();
            _controller = new CustomerController(_mockCustomerService.Object);
        }

        [Fact]
        public async void Get_WhenCalled_ReturnsAllItems()
        {
            //Arrange
            var readerMock = new Mock<IDataReader>();
            _mockSqlCommand.Setup(command => command.ExecuteReader()).Returns(readerMock.Object);
            _mockCustomerService.Setup(x => x.GetCustomers(CategoryEnum.ComputerStore)).ReturnsAsync(new List<Customer> { new Customer(), new Customer() } );
            //Act
            var result = await _controller.Get(CategoryEnum.ComputerStore);
            var okResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(okResult);
            Assert.True(result is OkObjectResult);
            Assert.IsType<List<Customer>>(okResult.Value);
            Assert.Equal(2, ((List<Customer>)okResult.Value).Count);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async void Get_WhenCalledNoCustomersExist_ReturnsNotFound()
        {
            //Arrange
            var readerMock = new Mock<IDataReader>();
            _mockSqlCommand.Setup(command => command.ExecuteReader()).Returns(readerMock.Object);
            _mockCustomerService.Setup(x => x.GetCustomers(CategoryEnum.ComputerStore)).ReturnsAsync(new List<Customer>());
            //Act
            var result = await _controller.Get(CategoryEnum.ComputerStore);
            var finalResult = result as NotFoundObjectResult;
            //Assert
            Assert.NotNull(finalResult);
            Assert.True(result is NotFoundObjectResult);
            Assert.IsType<string>(finalResult.Value);
            Assert.Equal(NoCustomersFound, finalResult.Value);
            Assert.Equal(StatusCodes.Status404NotFound, finalResult.StatusCode);
        }


    }
}
