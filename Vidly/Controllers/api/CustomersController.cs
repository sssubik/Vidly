using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.API
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _Context;
        public CustomersController() {
            _Context = new ApplicationDbContext();
        }
        //Get api/customers
        public IEnumerable<CustomerDto> GetCustomers() {
            return _Context.Customers
                .Include(c => c.MembershipType)
                .ToList().
                Select(Mapper.Map<Customer,CustomerDto>);
         }
        //Get api/customers/1
        public CustomerDto GetCustomer(int id)
        {
            var customer = _Context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else {
                return Mapper.Map<Customer,CustomerDto>(customer);
            }

        }
        //POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto) {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
                _Context.Customers.Add(customer);
                _Context.SaveChanges();
                customerDto.Id = customer.Id;
                return Created(new Uri(Request.RequestUri + "/" + customer.Id),customerDto);
            }
        }
        //PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                var customerInDb = _Context.Customers.SingleOrDefault(c => c.Id == id);
                if (customerInDb == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
                   

                    _Context.SaveChanges();
                }
            }
        }
        //Delete /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id) {
            var customerInDb = _Context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                _Context.Customers.Remove(customerInDb);
                _Context.SaveChanges();
            }
        }
    }
}
