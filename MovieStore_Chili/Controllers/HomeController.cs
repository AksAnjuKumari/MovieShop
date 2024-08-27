using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieStore_Chili.Data;
using MovieStore_Chili.Models;
using MovieStore_Chili.Models.Database;
using MovieStore_Chili.Models.ViewModels;
using MovieStore_Chili.Services;
using System.Diagnostics;

namespace MovieStore_Chili.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        private readonly IOrderService _orderService;
        private readonly MovieDbContext _db;
       
        public HomeController(ILogger<HomeController> logger, IMovieService movieService,IOrderService orderService, MovieDbContext db)
        {
            _logger = logger;
            _movieService = movieService;
            _orderService = orderService;
            _db= db;


        }

        public IActionResult Index()
        { 

		      return View();
        }
        public IActionResult SearchByTitle(string search)
        {
            var movieList = _movieService.GetMovies();

            if (!String.IsNullOrEmpty(search))
            {
                movieList = movieList.Where(m => m.Title.ToUpper().Contains(search.ToUpper())).ToList();
               
            }

            return View(movieList);
        }

        public IActionResult SortedByNewest()
        {
            var movieList = _movieService.GetMovies();
            FrontPageVM frontPageVM = new FrontPageVM()
            {
                NewestMovies = movieList.OrderByDescending(m => m.ReleaseYear).Take(5).ToList(),
            };
            return View(frontPageVM);
        }
        public IActionResult SortedByOldest() 
        {
            var movieList = _movieService.GetMovies();
            FrontPageVM frontPageVM = new FrontPageVM()
            {
                OldestMovies = movieList.OrderBy(m => m.ReleaseYear).Take(5).ToList(),
            };
            return View(frontPageVM);

        }
        public IActionResult SortedByCheapest()
        {
            var movieList = _movieService.GetMovies();
            FrontPageVM frontPageVM = new FrontPageVM()
            {
                CheapestMovies = movieList.OrderBy(m => m.Price).Take(5).ToList(),
            };
            return View(frontPageVM);
        }
        public IActionResult SortedByPopularMovies()
        {
           
            var popularmovies = _movieService.GetPopularMovies();
            FrontPageVM frontPageVM = new FrontPageVM()
            {
                PopulerMovies = popularmovies

            };


            return View(frontPageVM);

        }
        public IActionResult MostExpensiveOrder()
        {
            var mostExpensiveOrderCustomer =_orderService.FindCustomerWithMostExpensiveOrder();
            FrontPageVM frontPageVM = new FrontPageVM
            {
                MostExpensiveOrderCustomerrName = mostExpensiveOrderCustomer
            };
            return View(frontPageVM);

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
