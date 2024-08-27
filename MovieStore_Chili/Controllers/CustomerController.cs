using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MovieStore_Chili.Models.Database;
using MovieStore_Chili.Services;

namespace MyMovieShopProject.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public IActionResult CustomerSearch()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrShow(string Email)
        {
            Customer customer = _customerService.GetCustomerByEmail(Email);
            if (customer == null)
            {
                return RedirectToAction("Create");
            }
            else
            {
                HttpContext.Session.SetInt32("SelectedCustomer", customer.Id);
                return RedirectToAction("Detail", new { id = customer.Id });
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.AddCustomer(customer);
                customer = _customerService.GetCustomerByEmail(customer.Email);
                HttpContext.Session.SetInt32("SelectedCustomer", customer.Id);
                return RedirectToAction("OrderCreate", "Order");
            }
            return View();
        }

        public IActionResult Detail(int id)
        {
            Customer customer = _customerService.GetCustomerById(id);
            return View(customer);
        }

        public IActionResult Edit(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            _customerService.UpdateCustomer(customer);
            return RedirectToAction(nameof(Detail), new { id = customer.Id });
        }
    }
}




























