using MoviesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoviesApp.Controllers
{
    public class CustomerController : ApiController
    {
        private MoviesDBContext db;

        public CustomerController()
        {
            db = new MoviesDBContext();
        }
        // GET api/<controller>

        public IHttpActionResult GetCustomers()
        {
            var customers = db.Customers.ToList();
            return Ok(customers);


            
        }

        // GET api/<controller>/5


        // POST api/<controller>
        public IHttpActionResult PostMovie(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerId }, customer);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCustomer = db.Customers.FirstOrDefault(c => c.CustomerId == id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Age = customer.Age;

            db.SaveChanges();

            return Ok();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult DeleteMovie(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);

        }
    }
}