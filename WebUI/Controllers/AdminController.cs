using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using StoreBL;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IBL _bl;
        public AdminController(IBL bl)
        {
            _bl = bl;
        }

        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Customer()
        {
            List<Customer> allCustomers = _bl.GetAllCustomers();
            return View(allCustomers);
        }
        public ActionResult Store()
        {
            List<Store> allStores = _bl.GetAllStores();
            return View(allStores);
        }
        public ActionResult CustomerSearch()
        {
            IEnumerable<Order> customerOrders = _bl.GetAllOrders().Where(o => o.Customer.Name == Request.Form["CustomerSearch"]);

            return View(customerOrders);
        }
        public ActionResult StoreSearch()
        {
            IEnumerable<Order> storeOrders = _bl.GetAllOrders().Where(o => o.Store.Location == Request.Form["StoreSearch"]);

            return View(storeOrders);
        }
        public ActionResult Inventory()
        {
            IEnumerable<Inventory> inv = _bl.GetAllInventories().Where(i => i.Store.Location == Request.Form["Location"]);

            return View(inv);
        }
        public ActionResult UpdateInventory()
        {

            Order tempOrder = new Order()
            {
                StoreId = int.Parse(Request.Form["StoreID"]),
                LineItem = new LineItem()
                {
                    Quantity = int.Parse(Request.Form["Replenish"]),
                    ProductId = int.Parse(Request.Form["ProductID"])
                }
            };

            _bl.UpdateInventory(tempOrder);

            return RedirectToAction("SelectStore");
        }
        public ActionResult SelectStore()
        {
            return View();
        }
        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
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

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
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
