using System;
using Xunit;
using Models;

namespace Tests
{
    public class ModelTests
    {
        [Fact]
        public void CustomerShouldCreate()
        {
            Customer test = new Customer();

            Assert.NotNull(test);
        }

        [Fact]
        public void InventoryShouldCreate()
        {
            Inventory test = new Inventory();

            Assert.NotNull(test);
        }

        [Fact]
        public void LineItemShouldCreate()
        {
            LineItem test = new LineItem();

            Assert.NotNull(test);
        }

        [Fact]
        public void OrderShouldCreate()
        {
            Order test = new Order();

            Assert.NotNull(test);
        }

        [Fact]
        public void ProductShouldCreate()
        {
            Product test = new Product();

            Assert.NotNull(test);
        }

        [Fact]
        public void StoreShouldCreate()
        {
            Product test = new Product();

            Assert.NotNull(test);
        }
    }
}
