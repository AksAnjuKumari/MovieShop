using MovieStore_Chili.Models.Database;

namespace MovieStore_Chili.Services
{
    public interface IOrderService
    {
        List<Order> GetAllOrder();
        Order GetOrderById(int id);

        List<Order> GetCustomerOrders(string email);

        public List<Order> GetOrderInDetail();
        public Customer FindCustomerWithMostExpensiveOrder();

        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int orderId);
    }
}
