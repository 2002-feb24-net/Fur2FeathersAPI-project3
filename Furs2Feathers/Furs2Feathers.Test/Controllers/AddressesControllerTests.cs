using Xunit;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Furs2FeathersAPI.Controllers;
using Furs2Feathers.DataAccess.Repositories;
using Furs2Feathers.Domain.Interfaces;
using Furs2Feathers.Domain.Models;
using FluentAssertions;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Furs2Feathers.Test.Controllers
{
    public class AddressesControllerTests
    {
        public AddressesControllerTests()
        {
            addressRepo = new Mock<IAddressRepository>();
        }

        private Mock<IAddressRepository> addressRepo { get; set; }

        [Fact()]
        public async void GetAddresses()
        {
            // create a list of addresses to return
            var listOfAddresses = new List<Address>();

            listOfAddresses.Add(new Address()
            {
                AddressId = 1,
                Street = "123 Easy St",
                City = "New York",
                State = "NY",
                Country = "USA",
                Zip = "12345"
            } ) ;


            addressRepo.Setup(x => x.ToListAsync()).ReturnsAsync(listOfAddresses);
           
            // when ToListAsync is called, return this listOfaddresses instead (in order to test without dependencies)

            var addressesController = new AddressesController(addressRepo.Object);
            var result = await addressesController.GetAddress();
            var valuesOfresult = (result.Result as OkObjectResult).Value;
            valuesOfresult.Should().BeEquivalentTo(listOfAddresses);
            addressesController.Should().NotBeNull();

            /* var mockRepo = AddressRepo;
             mockRepo.Setup(ar => ar.FindAsyncAsNoTracking(1)).ReturnsAsync(Address)null);

             await subject.GetAddress(); // gets all addresses from the address controller action method

             mockRepo.Verify(ar => ar.ToListAsync(), Times.Exactly(1));*/
        }

        /* [Fact()]
         public async void GetSingleTest()
         {
             var mockRepo = AddressRepo;
             int testInt = 3;
             mockRepo.Setup(x => x.Consumable(testInt));

             await subject.GetAction(mockRepo.Object, testInt);

             mockRepo.Verify(x => x.Consumable(testInt), Times.Exactly(1));
         }

         [Fact()]
         public async void PostTest()
         {
             var post = AddressRepo;

             var consume = new ConsumableViewResource();

             post.Setup(x => x.AddConsumable(consume)).Returns(Task.Delay(10));

             var ret = await subject.PostAction(post.Object, consume);

             Assert.Equal(ret.Value, consume);

             post.Verify(x => x.AddConsumable(consume), Times.Exactly(1));
         }*/
    }
}
