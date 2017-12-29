using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _Context;
        public CustomerController() {
            _Context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _Context.Dispose();
        }
        // GET: Customer
        public ActionResult Index()
        {
            var Customers = _Context.Customers.Include(c => c.MembershipType).ToList();
            return View(Customers);
        }
        public ActionResult Details(int id)
        {
            var Customer = _Context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (Customer == null) {
                return HttpNotFound();
            }
            return View(Customer);
        }
        public ActionResult Display(int id)
        {
            var Customer = new Customer()
            {
                Name = "Subik",
                Id = id
            };
            return View(Customer);
        }
        
    }
}