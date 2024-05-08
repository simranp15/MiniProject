using EkartApplication.Models;

namespace EkartApplication.Repository
{
    public interface IOrderRepository
    {
        void PlaceOrder(Order order);
        Order GetOrderById(int orderId);
        List<Order> GetOrdersByCustomer(int customerId);
        Order GetHighestOrder();
        List<Order> GetOrdersByDate(DateTime orderDate);
    }
}
