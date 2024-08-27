using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore_Chili.Models.Database;
using MovieStore_Chili.Services;
using MovieStore_Chili.Helpers;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.EntityFrameworkCore;
using MovieStore_Chili.Data;
using System.Net.Mail;


namespace MovieStore_Chili.Controllers
{
    public class OrderController : Controller
    {

        const string sessionPassword = "Password";

        private readonly IMovieService _movieService;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly MovieDbContext _db;

        public OrderController(IMovieService movieService, IOrderService orderService, ICustomerService customerService, MovieDbContext db)
        {
            _movieService = movieService;
            _orderService = orderService;
            _customerService = customerService;
            _db = db;
        }


        public IActionResult Index()
        {


            return View();
        }

        public IActionResult AllOrder()
        {
            if (HttpContext.Session.Get<string>(sessionPassword) == default)
            {
                return RedirectToAction("Entrance");
            }

            var orders = _orderService.GetOrderInDetail();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Entrance()
        {
            return View();
        }

        public IActionResult OrderCreate()
        {
            Order order = new Order();
            order.CustomerId = Convert.ToInt32(HttpContext.Session.GetInt32("SelectedCustomer"));
            order.OrderDate = DateTime.Now;
            order.OrderRows = GetOrderRowsFromSession();


            _orderService.AddOrder(order);
            HttpContext.Session.Clear();

            return RedirectToAction("Detail", new { id = order.Id });

        }

        public IActionResult Detail(int id)
        {
            Order order = _orderService.GetOrderById(id);
            return View(order);
        }

        [HttpPost]
        public IActionResult Entrance(string inputPassword)
        {
            if (inputPassword == "chiLi")
            {
                HttpContext.Session.Set<string>(sessionPassword, inputPassword);
                return RedirectToAction("AllOrder");
            }

            return View();
        }



        public IActionResult SelectedMovieList()
        {
            List<OrderRow> orderRows = GetOrderRowsFromSession();
            return View(orderRows);
        }

        private List<OrderRow> GetOrderRowsFromSession()
        {
            List<OrderRow> orderRows = new List<OrderRow>();

            string? SelectedMovieIds = HttpContext.Session.GetString("SelectedMovieIds");
            if (SelectedMovieIds != null)
            {
                int[] movieIds = SelectedMovieIds.Split(',').Select(n => int.Parse(n)).ToArray();
                Array.Sort(movieIds);
                foreach (var movieId in movieIds)
                {
                    bool alredyAdded = false;
                    foreach (var orderRow in orderRows)
                    {
                        if (orderRow.MovieId == movieId)
                        {
                            alredyAdded = true;
                            orderRow.Quantity = orderRow.Quantity + 1;
                            orderRow.Price = orderRow.Movie.Price * orderRow.Quantity;
                            break;
                        }
                    }
                    if (!alredyAdded)
                    {
                        var movie = _movieService.GetMovieById(movieId);
                        OrderRow orderRow = new OrderRow();
                        orderRow.MovieId = movieId;
                        orderRow.Movie = movie;
                        orderRow.Quantity = 1;
                        orderRow.Price = movie.Price;
                        orderRows.Add(orderRow);
                    }
                }
            }
            return orderRows;
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
            return RedirectToAction("SelectedMovieList");
        }

        public IActionResult RemoveOneFromCart(int id)
        {
            string SelectedMovieIds = HttpContext.Session.GetString("SelectedMovieIds");
            string[] ids = SelectedMovieIds.Split(',');
            int indexToRemove = Array.IndexOf(ids, id.ToString());

            string newSelectedMovieIds = "";

            for (int i = 0; i < ids.Length; i++)
            {
                if (i != indexToRemove)
                {
                    if (newSelectedMovieIds.Length == 0)
                    {
                        newSelectedMovieIds = ids[i];
                    }
                    else
                    {
                        newSelectedMovieIds = newSelectedMovieIds + ',' + ids[i];
                    }
                }
            }

            if (newSelectedMovieIds.Length > 0)
            {
                HttpContext.Session.SetString("SelectedMovieIds", newSelectedMovieIds);
            }
            else
            {
                HttpContext.Session.Remove("SelectedMovieIds");
            }
            return RedirectToAction("SelectedMovieList");
        }

        public IActionResult RemoveFromCart(int id)
        {
            string SelectedMovieIds = HttpContext.Session.GetString("SelectedMovieIds");
            string[] ids = SelectedMovieIds.Split(',');

            string newSelectedMovieIds = "";

            for (int i = 0; i < ids.Length; i++)
            {
                if (ids[i] != id.ToString())
                {
                    if (newSelectedMovieIds.Length == 0)
                    {
                        newSelectedMovieIds = ids[i];
                    }
                    else
                    {
                        newSelectedMovieIds = newSelectedMovieIds + ',' + ids[i];
                    }
                }
            }

            if (newSelectedMovieIds.Length > 0)
            {
                HttpContext.Session.SetString("SelectedMovieIds", newSelectedMovieIds);
            }
            else
            {
                HttpContext.Session.Remove("SelectedMovieIds");
            }
            return Redirect("SelectedMovieList");
        }
        public IActionResult CustomersOrders()
        {
            return View();

        }
        [HttpPost]
        public IActionResult ShowCustomerOrders(string Email)
        {
            var customer = _customerService.GetCustomerByEmail(Email);

            if (customer == null)
            {
                return NotFound();
            }

            var customerOrders = _db.Orders
                .Where(o => o.CustomerId == customer.Id)
                .Include(o => o.OrderRows)
                    .ThenInclude(or => or.Movie)
                .ToList();

            return View(customerOrders);
        }

    }

}
