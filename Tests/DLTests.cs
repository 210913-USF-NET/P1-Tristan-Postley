using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entity = DL.Entities;
using Xunit;
using DL;
using Models;

namespace Tests
{
    public class DLTests
    {
        private readonly DbContextOptions<Entity.KrustyKrabDBContext> options;

        public DLTests()
        {
            //Construct db context options
            //using options builder everytime we instantiate DLTests class
            //using Sqlite's in memory db instead of real db 
            options = new DbContextOptionsBuilder<Entity.KrustyKrabDBContext>()
                .UseSqlite("Filename=Test.db").Options;
            
            Seed();
        }

        //Test Read operations
        [Fact]
        public void GetAllCustomersShouldGetAllCustomers()
        {
            using(Entity.KrustyKrabDBContext context = new Entity.KrustyKrabDBContext(options))
            {
                //Arrange
                IRepo repo = new DBRepo(context);

                //Act
                var customers = repo.GetAllCustomers();

                //Assert
                Assert.Equal(2, customers.Count);
            }
        }

        //For anything that modifies a data set (Write, Update, Delete)
        //Test using 2 contexts
        //1 to arrange and act
        //another to directly access the data with context
        public void AddingACustomerShouldAddCustomer()
        {
            using(Entity.KrustyKrabDBContext context = new Entity.KrustyKrabDBContext(options))
            {
                //Arrange
                IRepo repo = new DBRepo(context);
                Models.Customer custToAdd = new Models.Customer()
                {
                    Id = 3,
                    Name = "Test 3",
                    Password = "Pass 3"
                };
                repo.AddCustomer(custToAdd);

                //Act

                //Assert
            }

            // using(Entity.KrustyKrabDBContext context = new Entity.KrustyKrabDBContext(options))
            // {
            //     //Assert
            //     Entity.Customer cust =  context.Customers.FirstOrDefault(c => c.Id == 3);

            //     Assert.NotNull(cust);
            //     Assert.Equal(cust.Name, "Test 3");

            // }
        }

        private void Seed()
        {
            using(Entity.KrustyKrabDBContext context = new Entity.KrustyKrabDBContext(options))
            {
                //first, make sure db is in clean slate
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.AddRange(
                    new Entity.Customer()
                    {
                        Id = 1,
                        Name = "Test McGee",
                        Password = "shmassword"
                    },
                    new Entity.Customer()
                    {
                        Id = 2,
                        Name = "Test 2 ",
                        Password = "Pass 2"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}