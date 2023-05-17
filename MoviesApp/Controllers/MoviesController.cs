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
        /*
        public IHttpActionResult GetMovies(int page,int limit,int start)
        {

            var movies = db.Movies.ToList()
                .Select(m => new {
                    m.MovieId,
                    m.Title,
                    m.Description,
                    m.Genre,
                    ReleaseDate = m.ReleaseDate.HasValue? m.ReleaseDate.Value.ToString("yyyy-MM-dd") : ""
                })
                .OrderBy(m => m.MovieId)
                .Skip(start)
                .Take(limit)
                .ToList();

            int total = db.Movies.Count();

            var result = new
            {
                data = movies,
                total = total
            };

            return Ok(result);

            
        }*/



        /*[HttpGet]
        public IHttpActionResult GetMovies(int page, int limit, int start,[FromUri] List<Filter> filter)
        {
            var query = db.Movies.AsQueryable();
            
            if (filter != null && filter.Count > 0)
            {
                foreach (var f in filter)
                {
                    System.Diagnostics.Debug.WriteLine(f.property + ": "+ f.value);
                    if (!string.IsNullOrEmpty(f.value))
                    {
                        switch (f.property)
                        {
                            case "Title":
                                query = query.Where(m => m.Title.Contains(f.value));
                                System.Diagnostics.Debug.WriteLine(f.value);
                                break;
                            case "Genre":
                                query = query.Where(m => m.Genre.Contains(f.value));
                                break;
                        }
                    }
                }
            }

            var movies = query.ToList()
                .Select(m => new {
                    m.MovieId,
                    m.Title,
                    m.Description,
                    m.Genre,
                    ReleaseDate = m.ReleaseDate.HasValue ? m.ReleaseDate.Value.ToString("yyyy-MM-dd") : ""
                })
                .OrderBy(m => m.MovieId)
                .Skip(start)
                .Take(limit)
                .ToList();

            int total = query.Count();

            var result = new
            {
                data = movies,
                total = total
            };

            return Ok(result);
        }*/
        
        [HttpGet]
        public IHttpActionResult GetMovies(int page, int limit, int start, string filter = "")
        {
            var query = db.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(m => m.Title.Contains(filter));
            }

            var movies = query.ToList()
                .OrderBy(m => m.MovieId)
                .Skip(start)
                .Take(limit)
                .Select(m => new
                 {
                     m.MovieId,
                     m.Title,
                     m.Description,
                     m.Genre,
                     ReleaseDate = m.ReleaseDate.HasValue ? m.ReleaseDate.Value.ToString("yyyy-MM-dd") : ""
                 });
                
                

            int total = query.Count();

            var result = new
            {
                data = movies,
                total = total
            };

            return Ok(result);
        }
        

        [HttpPost]
        //[Route("api/Movies/AddMovie")]
        public IHttpActionResult PostMovie( [FromBody]Movie movie, string xd = "")
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



        [HttpDelete]
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