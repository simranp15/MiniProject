using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Question_1_Mvc.Models;

namespace Question_1_Mvc.Controllers
{
    public class CodeController : Controller
    {

        public NorthwindEntities db = new NorthwindEntities();
        // GET: Code
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GermanyCustomers()
        {
            var Germanycustomers = db.Customers.Where(c => c.Country == "Germany").ToList();
            return View(Germanycustomers);
        }
        public ActionResult CustomerOrder()
        {
            var OrderId = db.Customers.Where(c => c.Orders.Any(o => o.OrderID == 10248)).FirstOrDefault();

            return View(OrderId);
        }
    }
}