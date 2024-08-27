using MovieStore_Chili.Models.Database;

namespace MovieStore_Chili.Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomer();
        Customer GetCustomerById(int id);

        Customer GetCustomerByEmail(string email);

        void AddCustomer(Customer customer);
        void RemoveCustomer(Customer customer);

        void UpdateCustomer(Customer customer);
       


    }
}
