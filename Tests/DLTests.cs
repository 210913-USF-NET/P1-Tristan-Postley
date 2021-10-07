//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Entity = DL.Entities;
//using Xunit;
//using DL;
//using Models;
//using System.Linq;

//namespace Tests
//{
//    public class DLTests
//    {
//        private readonly DbContextOptions<Entity.KrustyKrabDBContext> options;

//        public DLTests()
//        {
//            //Construct db context options
//            //using options builder everytime we instantiate DLTests class
//            //using Sqlite's in memory db instead of real db 
//            options = new DbContextOptionsBuilder<Entity.KrustyKrabDBContext>()
//                .UseSqlite("Filename=Test.db").Options;
            
//            Seed();
//        }

//        //Test Read operations
//        [Fact]
//        public void GetAllCustomersShouldGetAllCustomers()
//        {
//            using(Entity.KrustyKrabDBContext context = new Entity.KrustyKrabDBContext(options))
//            {
//                //Arrange
//                IRepo repo = new DBRepo(context);

//                //Act
//                var customers = repo.GetAllCustomers();

//                //Assert
//                Assert.Equal(2, customers.Count);
//            }
//        }

//        [Fact]
//        public void GetAllStoresShouldGetAllStores()
//        {
//            using(Entity.KrustyKrabDBContext context = new Entity.KrustyKrabDBContext(options))
//            {
//                //Arrange
//                IRepo repo = new DBRepo(context);

//                //Act
//                var stores = repo.GetAllCustomers();

//                //Assert
//                Assert.Equal(2, stores.Count);
//            }
//        }

//        // [Fact]
//        // public void GetAllOrdersShouldGetAllOrders()
//        // {
//        //     using(Entity.KrustyKrabDBContext context = new Entity.KrustyKrabDBContext(options))
//        //     {
//        //         //Arrange
//        //         IRepo repo = new DBRepo(context);

//        //         //Act
//        //         var orders = repo.GetAllOrders();

//        //         //Assert
//        //         Assert.Equal(2, orders.Count);
//        //     }
//        // }

//        //For anything that modifies a data set (Write, Update, Delete)
//        //Test using 2 contexts
//        //1 to arrange and act
//        //another to directly access the data with context
//        [Fact]
//        public void AddingACustomerShouldAddCustomer()
//        {
//            using(Entity.KrustyKrabDBContext context = new Entity.KrustyKrabDBContext(options))
//            {
//                //Arrange
//                IRepo repo = new DBRepo(context);
//                Models.Customer custToAdd = new Models.Customer()
//                {
//                    Id = 3,
//                    Name = "Test 3",
//                    Password = "Pass 3"
//                };

//                //Act
//                repo.AddCustomer(custToAdd);

//            }
//            //Assert
//            using(var context = new Entity.KrustyKrabDBContext(options))
//            {
//                Entity.Customer cust = context.Customers.FirstOrDefault(c => c.Id == 3);

//                Assert.NotNull(cust);
//                Assert.Equal("Test 3", cust.Name);
//                Assert.Equal("Pass 3", cust.Password);

//            }
//        }

//        [Fact]
//        public void AddingAnOrderShouldAddOrder()
//        {
//            using(Entity.KrustyKrabDBContext context = new Entity.KrustyKrabDBContext(options))
//            {
//                //Arrange
//                IRepo repo = new DBRepo(context);
//                Models.Order orderToAdd = new Models.Order()
//                {
//                    Id = 3,
//                    Customer = new Customer(){Id = 2},
//                    Store = new Store(){Id = 2},
//                    Date = "9/28/2021"
//                };

//                //Act
//                repo.AddOrder(orderToAdd);

//            }
//            //Assert
//            using(var context = new Entity.KrustyKrabDBContext(options))
//            {
//                Entity.Order order = context.Orders.FirstOrDefault(o => o.Id == 3);

//                Assert.NotNull(order);
//                Assert.Equal(2, order.StoreId);
//                Assert.Equal(2, order.CustomerId);
//            }
//        }

//        private void Seed()
//        {
//            using(Entity.KrustyKrabDBContext context = new Entity.KrustyKrabDBContext(options))
//            {
//                //first, make sure db is in clean slate
//                context.Database.EnsureDeleted();
//                context.Database.EnsureCreated();

//                context.Customers.AddRange(
//                    new Entity.Customer()
//                    {
//                        Id = 1,
//                        Name = "Test McGee",
//                        Password = "shmassword"
//                    },
//                    new Entity.Customer()
//                    {
//                        Id = 2,
//                        Name = "Test 2 ",
//                        Password = "Pass 2"
//                    }
//                );

//                context.Stores.AddRange(
//                    new List<Entity.Store>{
//                        new Entity.Store()
//                        {
//                            Id = 1,
//                            Location = "Loc 1"
//                        },
//                        new Entity.Store()
//                        {
//                            Id = 2,
//                            Location = "Loc 2"
//                        }

//                    }
                    
//                );

//                context.Orders.AddRange(
//                    new List<Entity.Order>{
//                        new Entity.Order()
//                        {
//                            Id = 1,
//                            StoreId = 1,
//                            CustomerId = 1,
//                            Time = DateTime.Now
//                        },
//                        new Entity.Order()
//                        {
//                            Id = 2,
//                            StoreId = 2,
//                            CustomerId = 2,
//                            Time = DateTime.Now
//                        }

//                    }
                    
//                );

//                context.SaveChanges();
//            }
//        }
//    }
//}