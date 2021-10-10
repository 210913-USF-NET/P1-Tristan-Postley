using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreBL;
using Models;

namespace WebUI.Controllers
{
    public class OrderController : Controller
    {
        public static Order order = new Order();

        private IBL _bl;
        public OrderController(IBL bl)
        {
            _bl = bl;
        }

 

        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Location()
        {
            //Goto Admin Menu
            if(Request.Form["name"] == "Krabs" && Request.Form["password"] == "money")
            {
                return RedirectToAction("Index", "Admin");
            }
            List<Customer> allCustomers = _bl.GetAllCustomers();
            bool match = false;

            foreach (Customer cust in allCustomers)
            {
                if (cust.Name == Request.Form["name"] && cust.Password == Request.Form["password"])
                {
                    match = true;
                    HttpContext.Session.SetString("CustomerID", cust.Id.ToString());
                    HttpContext.Session.SetString("Customer", Request.Form["name"]);
                }
            }
            if (match) return View();
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLineItem()
        {
            System.Diagnostics.Debug.WriteLine("Add LineItem");
            System.Diagnostics.Debug.WriteLine(order.Id);

            order.LineItem = new LineItem();
            order.LineItem.Quantity = int.Parse(Request.Form["Quantity"]);
            List<Product> allProd =_bl.GetAllProducts();
            foreach (Product p in allProd)
            {
                if (p.Item == Request.Form["Product"])
                {
                    order.LineItem.Item = p;
                }
            }
            LineItem li = _bl.AddLineItem(order);
            System.Diagnostics.Debug.WriteLine(li.Id);

            return RedirectToAction("Create");

        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues loc = new();
                collection.TryGetValue("Location1", out loc);
                HttpContext.Session.SetString("Location", loc);


                List<Store> allStores = _bl.GetAllStores();
                foreach (Store s in allStores)
                {
                    if (s.Location == loc)
                    {
                        HttpContext.Session.SetString("StoreID", s.Id.ToString());

                    }
                }

                order.Store = new Store();
                order.Customer = new Customer();
                order.Store.Id = int.Parse(HttpContext.Session.GetString("StoreID"));
                order.Customer.Id = int.Parse(HttpContext.Session.GetString("CustomerID"));
                order = _bl.AddOrder(order);
                System.Diagnostics.Debug.WriteLine(order.Id);


                return RedirectToAction("create");
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
