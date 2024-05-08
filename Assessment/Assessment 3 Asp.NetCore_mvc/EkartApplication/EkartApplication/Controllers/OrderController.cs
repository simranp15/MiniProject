using Microsoft.AspNetCore.Mvc;
using EkartApplication.Repository;
using EkartApplication.Models;
using System;


public class OrderController : Controller
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult PlaceOrder()
    {
        return View();
    }

    [HttpPost]
    public IActionResult PlaceOrder(Order order)
    {
        if (ModelState.IsValid)
        {
            _orderRepository.PlaceOrder(order);
            return RedirectToAction(nameof(Index));
        }
        return View(order);
    }

    public IActionResult Details(int id)
    {
        var order = _orderRepository.GetOrderById(id);
        if (order == null)
        {
            return NotFound();
        }
        return View(order);
    }

    public IActionResult OrdersByDate(DateTime orderDate)
    {
        var orders = _orderRepository.GetOrdersByDate(orderDate);
        return View(orders);
    }

    public IActionResult OrdersByCustomer(int  customerId)
    {
        var orders = _orderRepository.GetOrdersByCustomer(customerId);
        return View(orders);
    }

    public IActionResult HighestOrder()
    {
        var highestOrder = _orderRepository.GetHighestOrder();
        return View(highestOrder);
    }
}
