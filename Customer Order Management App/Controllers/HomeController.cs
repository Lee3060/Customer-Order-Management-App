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


        //private int GetNextCustomerId()
        //{
        //    var existingIds = customerDb.Customers.Select(c => c.CustmerId).OrderBy(id => id).ToList();

        //    // If there are no existing IDs, start with 1
        //    if (existingIds.Count == 0)
        //    {
        //        return 1;
        //    }

        //    // Find the smallest missing ID
        //    for (int i = 1; i <= existingIds.Count; i++)
        //    {
        //        if (existingIds[i - 1] != i)
        //        {
        //            return i;
        //        }
        //    }

        //    // If there are no gaps, return the next ID after the highest
        //    return existingIds.Count + 1;
        //}
        private int GetNextCustomerId()
        {
            return customerDb.Customers.Any() ? customerDb.Customers.Max(c => c.CustmerId) + 1 : 1;
        }




        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(Customer cust)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await customerDb.Customers.AddAsync(cust);
        //        await customerDb.SaveChangesAsync();
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View(cust);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Customer cust)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (var transaction = customerDb.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                // Generate the next available CustomerId
        //                cust.CustmerId = GetNextCustomerId();
        //                customerDb.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Customers ON");
        //                customerDb.Customers.Add(cust);
        //                customerDb.SaveChanges();
        //                customerDb.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Customers OFF");
        //                transaction.Commit();
        //            }
        //            catch (Exception)
        //            {
        //                transaction.Rollback();
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index","Home");
        //    }

        //    return View(cust);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer cust)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = customerDb.Database.BeginTransaction())
                {
                    try
                    {
                        // Generate the next available CustomerId
                        cust.CustmerId = GetNextCustomerId();

                        // Enable IDENTITY_INSERT for the Customers table
                        customerDb.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Customers ON");

                        customerDb.Customers.Add(cust);
                        customerDb.SaveChanges();

                        // Disable IDENTITY_INSERT for the Customers table
                        customerDb.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Customers OFF");

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                return RedirectToAction("Index");
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

        public async Task<IActionResult> Delete(int id)
        {
            if(id == null || customerDb.Customers == null)
            {
                return NotFound();
            }
            var custData = await customerDb.Customers.FirstOrDefaultAsync(x => x.CustmerId == id);
            if (custData == null)
            {
                return NotFound();
            }
            return View(custData);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var custData = await customerDb.Customers.FindAsync(id);
            if(custData != null)
            {
                customerDb.Customers.Remove(custData);
            }
            await customerDb.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
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
