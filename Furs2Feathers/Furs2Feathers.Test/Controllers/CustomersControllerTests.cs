using FluentAssertions;
using Furs2Feathers.Domain.Interfaces;
using Furs2Feathers.Domain.Models;
using Furs2FeathersAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Furs2Feathers.Test.Controllers
{
    public class CustomersControllerTests
    {

        public CustomersControllerTests()
        {
            customerRepo = new Mock<ICustomerRepository>();
        }

        private Mock<ICustomerRepository> customerRepo { get; set; }
        private readonly ILogger<CustomersController> logger;

        [Fact]
        public async void GetCustomer()
        {
            var listOfCustomers = new List<Customer>();

            var customerController = new CustomersController(customerRepo.Object, logger);
            var result = await customerController.GetCustomers();
            var valuesOfresult = (result.Result as OkObjectResult).Value;
            valuesOfresult.Should().BeEquivalentTo(listOfCustomers);
            customerController.Should().NotBeNull();
        }
    }
}
