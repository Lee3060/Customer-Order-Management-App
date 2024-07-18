using Customer_Order_Management_App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Customer_Order_Management_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly CustomerDbContext customerDb;

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public HomeController(CustomerDbContext customerDb)
        {
            this.customerDb = customerDb;
        }

        public IActionResult Index()
        {
            var custData = customerDb.Customers.ToList();
            return View(custData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
