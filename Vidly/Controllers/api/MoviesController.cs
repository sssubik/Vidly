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

namespace Vidly.Controllers.api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _Context;
        public MoviesController() {
            _Context = new ApplicationDbContext();
        }

        public IEnumerable<Movies> GetMovies()
        {
            return _Context.Movies.Include(c => c.Genre).ToList();
        }
        public MoviesDto GetMovie(int id)
        {
            var movies = _Context.Movies.SingleOrDefault(c => c.Id ==id);
            if (movies == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else {
                return Mapper.Map<Movies, MoviesDto>(movies);
            }
        }
        [HttpPost]
        public IHttpActionResult CreateMovies(MoviesDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var movie = Mapper.Map<MoviesDto, Movies>(movieDto);
                _Context.Movies.Add(movie);
                _Context.SaveChanges();
                movieDto.Id = movie.Id;
                return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
            }

        }
        [HttpPut]
        public void UpdateMovie(int id, MoviesDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            else
            {
                var movieInDb = _Context.Movies.SingleOrDefault(c => c.Id == id);
                if (movieInDb == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
                else
                {
                    Mapper.Map<MoviesDto, Movies>(movieDto, movieInDb);


                    _Context.SaveChanges();
                }
            }
        }
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movieInDb = _Context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                _Context.Movies.Remove(movieInDb);
                _Context.SaveChanges();
            }
        }

    }
}
