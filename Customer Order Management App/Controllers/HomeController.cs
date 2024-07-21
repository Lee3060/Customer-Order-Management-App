using Customer_Order_Management_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;

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

        public async Task<IActionResult> Index()
        {
            var custData = await customerDb.Customers.ToListAsync();
            return View(custData);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer cust)
        {
            if (ModelState.IsValid)
            {
                await customerDb.Customers.AddAsync(cust);
                await customerDb.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(cust);
        }

        public async Task<IActionResult> Details(int? id)  //Due to ? if id can be null
        {
            if(id == null || customerDb.Customers == null)
            {
                return NotFound();
            }
            var custData = await customerDb.Customers.FirstOrDefaultAsync(x => x.CustmerId == id);
            if(customerDb == null)
            {
                return NotFound();
            }
            return View(custData);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || customerDb.Customers == null)
            {
                return NotFound();
            }
            var custData = await customerDb.Customers.FindAsync(id);
            if (customerDb == null)
            {
                return NotFound();
            }
            return View(custData);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int? id, Customer cust)
        {
            if(id != cust.CustmerId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                customerDb.Customers.Update(cust);
                await customerDb.SaveChangesAsync();
                return RedirectToAction("Index", "Home");   
            }
            return View(cust);
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
