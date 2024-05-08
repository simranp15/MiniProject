using EkartApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EkartApplication.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NorthwindContext _context;

        public OrderRepository(NorthwindContext context)
        {
            _context = context;
        }

        public void PlaceOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public List<Order> GetOrdersByCustomer(int customerId)
        {
            return _context.Orders.Where(o => o.CustomerId == customerId).ToList();
        }

        public Order GetHighestOrder()
        {
            return _context.Orders.OrderByDescending(o => o.TotalAmount).FirstOrDefault();
        }

        public List<Order> GetOrdersByDate(DateTime orderDate)
        {
            throw new NotImplementedException();
        }

        //  public List<Order> GetOrdersByDate(DateTime orderDate)
        // {
        //  return _context.Orders.Where(o => o.OrderDate?.Date == orderDate.Date).ToList();
        // }
    }
}