using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using WebUI.Controllers;
using StoreBL;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Tests
{
    public class ControllerTests
    {
        [Fact]
        public void CustomerControllerIndexShouldReturnListOfCustomers()
        {
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.GetAllCustomers()).Returns(
                new List<Customer>()
                {
                    new Customer()
                    {
                        Id = 1,
                        Name = "Test",
                        Password = "pass"
                    },
                    new Customer()
                    {
                        Id = 2,
                        Name = "Test2",
                        Password = "pass2"
                    }
                }
            );
            var controller = new CustomerController(mockBL.Object);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<Customer>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void AdminControllerCustomerShouldReturnListOfCustomers()
        {
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.GetAllCustomers()).Returns(
                new List<Customer>()
                {
                    new Customer()
                    {
                        Id = 1,
                        Name = "Test",
                        Password = "pass"
                    },
                    new Customer()
                    {
                        Id = 2,
                        Name = "Test2",
                        Password = "pass2"
                    }
                }
            );
            var controller = new AdminController(mockBL.Object);

            var result = controller.Customer();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<Customer>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void AdminControllerStoreShouldReturnListOfStores()
        {
            var mockBL = new Mock<IBL>();
            mockBL.Setup(x => x.GetAllStores()).Returns(
                new List<Store>()
                {
                    new Store()
                    {
                        Id = 1,
                        Location = "test"
                    },
                    new Store()
                    {
                        Id = 2,
                        Location = "test2"
                    }
                }
            );
            var controller = new AdminController(mockBL.Object);

            var result = controller.Store();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<Store>>(viewResult.ViewData.Model);

            Assert.Equal(2, model.Count());
        }
        //[Fact]
        //public void AdminControllerInventoryShouldReturnListOfInventories()
        //{
        //    var mockBL = new Mock<IBL>();
        //    mockBL.Setup(x => x.GetAllInventories()).Returns(
        //        new List<Inventory>()
        //        {
        //            new Inventory()
        //            {
        //                Store = new Store()
        //                {
        //                    Location = "test"
        //                }
        //            },
        //            new Inventory()
        //            {
        //                Store = new Store()
        //                {
        //                    Location = "test"
        //                }
        //            }
        //        }
        //    );

        //    var formData = new FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues> { { "Location", "test" } });

        //    var requestMock = new Mock<HttpRequest>();
        //    requestMock.SetupGet(x => x.Form).Returns(formData);

        //    var contextMock = new Mock<HttpContext>();
        //    contextMock.SetupGet(x => x.Request).Returns(requestMock.Object);


        //    var controller = new AdminController(mockBL.Object);

        //    var result = controller.Inventory();

        //    var viewResult = Assert.IsType<ViewResult>(result);

        //    var model = Assert.IsAssignableFrom<IEnumerable<Inventory>>(viewResult.ViewData.Model);

        //    Assert.Equal(2, model.Count());
        //}
    }
}
