using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStore_Chili.Data;
using MovieStore_Chili.Models.Database;
using MovieStore_Chili.Services;
using PagedList;

namespace MovieStore_Chili.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public IActionResult Index(int? page)
        {
            int pageNumber = (page ?? 1);
            var movies = _movieService.GetMovies();
            var moviePage = movies.ToPagedList(pageNumber, 4);
            return View(moviePage);
        }   
        public IActionResult Admin()
        {
            var movies = _movieService.GetMovies();
            return View(movies);
        }

        public IActionResult AddToCart(int id)
        {
            string SelectedMovieIds = HttpContext.Session.GetString("SelectedMovieIds");
            if (SelectedMovieIds == null)
            {
                SelectedMovieIds = id.ToString();
            }
            else
            {
                SelectedMovieIds = SelectedMovieIds + "," + id.ToString();
            }
            HttpContext.Session.SetString("SelectedMovieIds", SelectedMovieIds);
            return Redirect("Index");
        }
        public IActionResult AddToCartFromDashBoard(int id)
        {
            string SelectedMovieIds = HttpContext.Session.GetString("SelectedMovieIds");
            if (SelectedMovieIds == null)
            {
                SelectedMovieIds = id.ToString();
            }
            else
            {
                SelectedMovieIds = SelectedMovieIds + "," + id.ToString();
            }
            HttpContext.Session.SetString("SelectedMovieIds", SelectedMovieIds);
            return Json(new { success = true });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movieService.AddMovie(movie);
                return RedirectToAction(nameof(Admin));
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            var movie = _movieService.GetMovieById(id);
            return View(movie);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _movieService.RemoveMovie(id);
            return RedirectToAction(nameof(Admin));
        }
        public IActionResult Edit(int id)
        {
            var movie = _movieService.GetMovieById(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {
            _movieService.UpdateMovie(movie);
            return RedirectToAction(nameof(Admin));
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        
    }
}
