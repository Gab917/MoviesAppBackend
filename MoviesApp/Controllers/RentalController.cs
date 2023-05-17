using MoviesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace MoviesApp.Controllers
{
    public class RentalController : ApiController
    {
        // GET api/<controller>
        private MoviesDBContext db;

        public RentalController()
        {
            db = new MoviesDBContext();
        }

        /*[HttpGet]
        public IHttpActionResult GetRentals()
        {
            var rentals = db.Rentals.Include(r => r.Customer).Include(r => r.Movie).ToList();
            return Ok(rentals);
        }*/

        /*[HttpGet]
        public IHttpActionResult GetRentals()
        {
            var rentals = db.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Movie.Title)
                .ToList();
            return Ok(rentals);
        }*/

        [HttpGet]
        public IHttpActionResult GetRentals(int page, int limit, int start, string filter = "", string property = "")
        {
            var query = db.Rentals.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                switch (property) {
                    case "CustomerName":
                        query = query.Where(r => r.Customer.FullName.Contains(filter));
                        break;
                    case "MovieTitle":
                        query = query.Where(r => r.Movie.Title.Contains(filter));
                        break;
                    case "RentalDate":
                        DateTime rentalDate;
                        DateTime.TryParse(filter, out rentalDate);
                        query = query.Where(r => r.RentalDate == rentalDate.Date);
                        break;
                    case "CustomerId":
                        if (int.TryParse(filter, out int customerId))
                        {
                            if (customerId == null) {
                                return BadRequest();
                            }
                            query = query.Where(r => r.RentalId == customerId);
                        }
                        break;
                }

            }

            var rentals = query
                .Include(r => r.Movie)
                .Include(r => r.Customer)
                .OrderBy(r => r.RentalId)
                .Skip(start)
                .Take(limit)
                .ToList()  // retrieve the data from the database
                .Select(r => new
                {
                    RentalId = r.RentalId,
                    RentalDate = r.RentalDate.ToString("yyyy-MM-dd"),  // format the date
                    ReturnDate = r.ReturnDate.HasValue ? r.ReturnDate.Value.ToString("yyyy-MM-dd") : "",
                    CustomerId = r.CustomerId,
                    CustomerName = r.Customer.FullName,
                    MovieId = r.MovieId,
                    MovieTitle = r.Movie.Title
                })
                .ToList();  // materialize the query

            int total = query.Count();

            var result = new
            {
                data = rentals,
                total = total

            };
            return Ok(result);
        }
        //[HttpGet]
        [HttpGet]
        [Route("api/Rental/GetRentalsByCustomerId")]
        public IHttpActionResult GetRentalsByCustomerId(int customerId)
        {

            var rentals = db.Rentals
                .Include(r => r.Movie)
                .Include(r => r.Customer)
                .Where(r => r.CustomerId == customerId && r.ReturnDate == null)
                .Select(r => new
                {
                    RentalId = r.RentalId,
                    RentalDate = r.RentalDate,
                    ReturnDate = r.ReturnDate,
                    CustomerId = r.CustomerId,
                    CustomerName = r.Customer.FullName,
                    MovieId = r.MovieId,
                    MovieTitle = r.Movie.Title
                })
                .ToList();

            return Ok(rentals);
        }

        [HttpPost]
        public IHttpActionResult RentMovies([FromBody] RentMoviesDTO rentMoviesDTO) //RentMoviesDTO to hold values of CustomerId and array of MovieIds selected.
        {
            // Retrieve customer entity from the database
            var customer = db.Customers.SingleOrDefault(c => c.CustomerId == rentMoviesDTO.CustomerId);

            if (customer == null) { // Checks if there customerId is present in the database
                return BadRequest("Customer ID not found");
            }

            if (rentMoviesDTO.MovieIds == null || !rentMoviesDTO.MovieIds.Any())
            {
                return BadRequest("No Movies selected");
            }

            // Retrieve movie entities from the database
            var movies = db.Movies.Where(m => rentMoviesDTO.MovieIds.Contains(m.MovieId)).ToList();

            // Create rental entities for each selected movie and assign the customer and movie entities to it
            var rentals = new List<Rental>();
            foreach (var movie in movies)
            {
                var rental = new Rental()
                {
                    Customer = customer,
                    Movie = movie,
                    RentalDate = DateTime.Now
                };
                rentals.Add(rental);
            }

            // Save the rental entities to the database
            db.Rentals.AddRange(rentals);
            db.SaveChanges();

            return Ok();
        }

        /*[HttpPost]
        public IHttpActionResult RentMovies(int customerId, [FromBody] IEnumerable<int> movieIds)
        {
            // Retrieve customer entity from the database
            var customer = db.Customers.SingleOrDefault(c => c.CustomerId == customerId);

            if (movieIds == null || !movieIds.Any())
            {
                return BadRequest();
            }
            // Retrieve movie entities from the database
            var movies = db.Movies.Where(m => movieIds.Contains(m.MovieId)).ToList();

            // Create rental entities for each selected movie and assign the customer and movie entities to it
            var rentals = new List<Rental>();
            foreach (var movie in movies)
            {
                var rental = new Rental()
                {
                    Customer = customer,
                    Movie = movie,
                    RentalDate = DateTime.Now
                };
                rentals.Add(rental);
            }

            // Save the rental entities to the database
            db.Rentals.AddRange(rentals);
            db.SaveChanges();

            return Ok();
        }*/

        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/



        /* POST api/<controller>
        public void Post([FromBody] string value)
        {
        }
        */

        
        [HttpPost]
        [Route("api/Rental/ReturnMovies")]
        public IHttpActionResult ReturnMovies(ReturnMoviesDTO returnMoviesDTO)
        {
            var rentals = db.Rentals.Where(r => returnMoviesDTO.RentalIds.Contains(r.RentalId)).ToList();

            foreach (var rental in rentals) 
            { 
                rental.ReturnDate = DateTime.Now;
            }

            db.SaveChanges();

            return Ok();

        }

        [HttpPut]
        public IHttpActionResult UpdateRental(int id, Rental rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingRental = db.Rentals.FirstOrDefault(r => r.RentalId == id);

            if (existingRental == null)
            {
                return NotFound();
            }

            existingRental.CustomerId = rental.CustomerId;
            existingRental.RentalDate = rental.RentalDate;
            existingRental.MovieId = rental.MovieId;
            existingRental.ReturnDate = rental.ReturnDate;

            db.SaveChanges();

            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult DeleteRental(int id)
        {
            Rental rental = db.Rentals.Find(id);
            if (rental == null)
            {
                return NotFound();
            }

            db.Rentals.Remove(rental);
            db.SaveChanges();

            return Ok(rental);

        }
    }
}