using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rent_a_car_mvc.Data;
using rent_a_car_mvc.Models;

namespace rent_a_car_mvc.Controllers
{
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/[controller]/[action]")]
        public IActionResult Index()
        {
            using var context = new RentACarContext();
            var customers = context.Customers.ToList();
            return View("Index", customers);
        }
        [HttpGet("/[controller]/[action]/{id}")]
        public IActionResult Details(int id)
        {
            using var context = new RentACarContext();
            var customerToSeeDetails = context.Customers.Find(id);
            return View("Details", customerToSeeDetails);
        }
        [HttpGet("/[controller]/[action]/{id}")]
        public IActionResult Edit(int id)
        {
            using var context = new RentACarContext();
            var customerToBeUpdated = context.Customers.Find(id);
            return View(customerToBeUpdated);
        }
        [HttpPost("/[controller]/[action]/{id}")]
        public IActionResult Edit(Customer editedCustomer)
        {
            using var context = new RentACarContext();
            if (ModelState.IsValid)
            {
                context.Customers.Update(editedCustomer);
                context.SaveChanges();
                return RedirectToAction("Details", new { id = editedCustomer.Id });
            }
            return View(editedCustomer);
        }
        [HttpGet("/[controller]/[action]")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("/[controller]/[action]")]
        public IActionResult Create(Customer createdCustomer)
        {
            using var context = new RentACarContext();
            if (ModelState.IsValid)
            {
                context.Customers.Add(createdCustomer);
                context.SaveChanges();
                return RedirectToAction("Details", new { id = createdCustomer.Id });
            }
            return View(createdCustomer);
        }
        [HttpPost("/[controller]/[action]/{id}")]
        public IActionResult Delete(int id)
        {
            using var context = new RentACarContext();
            var customerToDelete = context.Customers.Find(id);
            context.Customers.Remove(customerToDelete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}