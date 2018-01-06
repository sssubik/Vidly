using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;

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
            return View();
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
        public ActionResult New() {
            var membershipTypes = _Context.MembershipTypes.ToList();
            var viewModel = new NewCustomerViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm",viewModel);
        }
        [HttpPost]
        public ActionResult Save(Customer customer) {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewCustomerViewModel()
                {
                    MembershipTypes = _Context.MembershipTypes.ToList(),
                    Customer = customer
                    
                };
                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
            {
                _Context.Customers.Add(customer);
            }
            else {
                var customerInDb = _Context.Customers.Single(c => c.Id == customer.Id);
                
                customerInDb.Name = customer.Name;
                customerInDb.DOB = customer.DOB;
                customerInDb.isSubscribedToNewsletter = customer.isSubscribedToNewsletter;
                
                customerInDb.MembershipTypeId = customer.MembershipTypeId;

            }
            _Context.SaveChanges();
            return RedirectToAction("Index","Customer");
        }
        public ActionResult Edit(int id) {
            var customer = _Context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null) {
                return HttpNotFound();
                
            }
            var viewModel = new NewCustomerViewModel
            {
                Customer = customer,
                MembershipTypes = _Context.MembershipTypes.ToList()
            };
            return View("CustomerForm",viewModel);
        }
        
    }
}