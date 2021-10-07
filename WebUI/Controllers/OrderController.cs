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

            //System.Diagnostics.Debug.WriteLine(Request.Form["name"]);
            //System.Diagnostics.Debug.WriteLine(Request.Form["password"]);
            //System.Diagnostics.Debug.WriteLine(ViewBag.Customer);

            //ViewBag.Customer = Request.Form["name"];
            return View();
        }
        public ActionResult StartOrder()
        {
            System.Diagnostics.Debug.WriteLine("TEST");
            System.Diagnostics.Debug.WriteLine(Request.Form["name"]);
            System.Diagnostics.Debug.WriteLine(Request.Form["password"]);
            //order.Customer.Name = Request.Form["name"];
            //ViewBag["Customer"] = new Customer(Request.Form["name"], Request.Form["password"]);
            return RedirectToAction("create");
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToOrder()
        {
            System.Diagnostics.Debug.WriteLine(Request.Form["Location"]);
            System.Diagnostics.Debug.WriteLine(Request.Form["Product"]);
            return RedirectToAction("Create");

        }

        // POST: OrderController/Create
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
