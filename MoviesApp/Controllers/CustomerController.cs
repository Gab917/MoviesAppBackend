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

        public IHttpActionResult GetCustomers(int page, int limit, int start, string filter = "")
        {
            var query = db.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(c => c.FullName.Contains(filter));
            }

            var customers = query.ToList()
                .OrderBy(m => m.CustomerId)
                .Skip(start)
                .Take(limit)
                .Select(c => new
                {
                    c.CustomerId,
                    c.FullName,
                    c.EmailAddress,
                    c.Age
                });



            int total = query.Count();

            var result = new
            {
                data = customers,
                total = total
            };

            return Ok(result);

            
            
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

            existingCustomer.FullName = customer.FullName;
            existingCustomer.EmailAddress= customer.EmailAddress;
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