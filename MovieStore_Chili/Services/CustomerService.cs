using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MovieStore_Chili.Data;
using MovieStore_Chili.Models.Database;
using System.Net.Mail;

 namespace MovieStore_Chili.Services
 {
     public class CustomerService : ICustomerService

    {
        private readonly MovieDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IMovieService _movieService;

        public int Id { get; private set; }

        public CustomerService(MovieDbContext db, IConfiguration configuration, IMovieService movieService)
        {
            _db = db;
            _configuration = configuration;
            _movieService = movieService;
        }
        public List<Customer> GetCustomer()
        {
            var customer = _db.Customers.ToList();
            return customer;
        }

        public Customer GetCustomerById(int id)
        {
            var customer = _db.Customers.FirstOrDefault(c => c.Id == id);
           
            return customer;
        }
        public Customer GetCustomerByEmail(string email)
        {
           
            var customer = _db.Customers.FirstOrDefault(c => c.Email == email);
            return customer;
         

           

       }
        public void AddCustomer(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
        }

        public void RemoveCustomer(Customer customer)
        {
            _db.Customers.Remove(customer);
            _db.SaveChanges();
        }
        public void RemoveCustomer(int id)
        {
            var customer = _db.Customers.First(c => c.Id == id);
            _db.Customers.Remove(customer);
            _db.SaveChanges();
           
        }

        public void UpdateCustomer(Customer customer)
        {
            _db.Customers.Update(customer);
            _db.SaveChanges();
        }

        

    }   
 }
