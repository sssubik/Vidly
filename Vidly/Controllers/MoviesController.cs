using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;

namespace VidlyUdemy.Controllers
{
    public class MoviesController : Controller
    {
        public ApplicationDbContext _Context;

        public MoviesController() {
            _Context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _Context.Dispose();
        }
        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movies()
            {
                Name = "Shrek!"
            };
            var customers = new List<Customer>() {
                new Customer { Name = "Subik"},
                new Customer { Name = "Rajan"}
            };
            var viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers = customers
            };


            return View(viewModel);
        }
        
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            var Movies = _Context.Movies.Include(c => c.Genre).ToList();
            return View(Movies);
        }
        [Route("Movies/Released/{year}/{month:regex(\\d{4}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }
        public ActionResult Details(int id)
        {
            var Movies = _Context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
            return View(Movies);
           
        }
        public ActionResult Display(int id)
        {
            var Movie = new Movies() {
                Name = "Shrek!!",
                Id = id
            };

            return View(Movie);
        }
    }
}