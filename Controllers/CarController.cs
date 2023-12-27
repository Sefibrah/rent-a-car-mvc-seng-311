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
    public class CarController : Controller
    {
        private readonly ILogger<CarController> _logger;

        public CarController(ILogger<CarController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/[controller]/[action]")]
        public IActionResult Index()
        {
            using var context = new RentACarContext();
            var cars = context.Cars.ToList();
            return View("Index", cars);
        }
        [HttpGet("/[controller]/[action]/{id}")]
        public IActionResult Details(int id)
        {
            using var context = new RentACarContext();
            var carToSeeDetails = context.Cars.Find(id);
            return View("Details", carToSeeDetails);
        }
        [HttpGet("/[controller]/[action]/{id}")]
        public IActionResult Edit(int id)
        {
            using var context = new RentACarContext();
            var carToBeUpdated = context.Cars.Find(id);
            return View(carToBeUpdated);
        }
        [HttpPost("/[controller]/[action]/{id}")]
        public IActionResult Edit(Car editedCar)
        {
            using var context = new RentACarContext();
            if (ModelState.IsValid)
            {
                context.Cars.Update(editedCar);
                context.SaveChanges();
                return RedirectToAction("Details", new { id = editedCar.Id });
            }
            return View(editedCar);
        }
        [HttpGet("/[controller]/[action]")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("/[controller]/[action]")]
        public IActionResult Create(Car createdCar)
        {
            using var context = new RentACarContext();
            if (ModelState.IsValid)
            {
                context.Cars.Add(createdCar);
                context.SaveChanges();
                return RedirectToAction("Details", new { id = createdCar.Id });
            }
            return View(createdCar);
        }
        [HttpPost("/[controller]/[action]/{id}")]
        public IActionResult Delete(int id)
        {
            using var context = new RentACarContext();
            var carToDelete = context.Cars.Find(id);
            context.Cars.Remove(carToDelete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}