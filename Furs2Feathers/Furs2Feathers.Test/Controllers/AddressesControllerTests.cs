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

namespace Furs2Feathers.Test.Controllers
{
    public class AddressesControllerTests
    {
        public AddressesControllerTests()
        {
            Mock<ILogger<AddressesController>> log = new Mock<ILogger<AddressesController>>();
            subject = new AddressesController(AddressRepo.Object);
        }

        private Mock<IAddressRepository> AddressRepo => new Mock<IAddressRepository>();
        private AddressesController subject;

        [Fact()]
        public async void GetListTest()
        {
            var get = AddressRepo;
            get.Setup(ar => ar.ToListAsync());

            await subject.GetAddress(); // gets all addresses from the address controller action method

            get.Verify(ar => ar.ToListAsync(), Times.Exactly(1));
        }

       /* [Fact()]
        public async void GetSingleTest()
        {
            var get = AddressRepo;
            int testInt = 3;
            get.Setup(x => x.Consumable(testInt));

            await subject.GetAction(get.Object, testInt);

            get.Verify(x => x.Consumable(testInt), Times.Exactly(1));
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
