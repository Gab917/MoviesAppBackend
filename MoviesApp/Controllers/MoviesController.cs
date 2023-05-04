using MoviesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoviesApp.Controllers
{
    public class MoviesController : ApiController
    {

        private MoviesDBContext db;

        public MoviesController()
        {
            db = new MoviesDBContext();
        }

        public IHttpActionResult GetMovies()
        {
            //var movies = db.Movies.ToList();
            //return Ok(movies);

            /*var movies = db.Movies.Select(m => new {
                m.Title,
                m.Description,
                m.Genre,
                ReleaseDate = m.ReleaseDate.ToString("yyyy-MM-dd")
            }).ToList();*/

            var movies = db.Movies.ToList()
                .Select(m => new {
                    m.MovieId,
                    m.Title,
                    m.Description,
                    m.Genre,
                    ReleaseDate = m.ReleaseDate.HasValue? m.ReleaseDate.Value.ToString("yyyy-MM-dd") : ""
                })
                .ToList();

            return Ok(movies);

            //
        }


        public IHttpActionResult PostMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movies.Add(movie);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = movie.MovieId }, movie);
        }


        // PUT api/<controller>/5
        /*public void Put(int id, [FromBody] string value)
        {
        }*/

        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingMovie = db.Movies.FirstOrDefault(m => m.MovieId == id);

            if (existingMovie == null)
            {
                return NotFound();
            }

            existingMovie.Title = movie.Title;
            existingMovie.Description = movie.Description;
            existingMovie.Genre = movie.Genre;
            existingMovie.ReleaseDate = movie.ReleaseDate;

            db.SaveChanges();

            return Ok();
        }

        // DELETE api/<controller>/5




        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movie);
            db.SaveChanges();

            return Ok(movie);

        }
    }
}