using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using rent_a_car_mvc.Data;
using rent_a_car_mvc.Dtos;
using rent_a_car_mvc.Models;

namespace rent_a_car_mvc.Controllers
{
    [Route("[controller]")]
    public class ContractController : Controller
    {
        private readonly ILogger<ContractController> _logger;

        public ContractController(ILogger<ContractController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/[controller]/[action]")]
        public IActionResult Index()
        {
            using var context = new RentACarContext();
            var contractsList = context.Contracts
                .Join(
                    context.Cars,
                    contract => contract.CarId,
                    car => car.Id,
                    (contract, car) => new
                    {
                        Contract = contract,
                        Car = car
                    })
                .Join(
                    context.Customers,
                    joinResult => joinResult.Contract.RenterId,
                    renter => renter.Id,
                    (joinResult, renter) => new
                    {
                        joinResult.Contract,
                        joinResult.Car,
                        Renter = renter
                    })
                .Join(
                    context.Customers,
                    joinResult => joinResult.Contract.DriverId,
                    driver => driver.Id,
                    (joinResult, driver) => new ContractDto
                    {
                        Id = joinResult.Contract.Id,
                        Car = joinResult.Car.Make + " " + joinResult.Car.Model + " (" + joinResult.Car.RegistrationPlate + ")",
                        Renter = joinResult.Renter.Name,
                        Contact = joinResult.Renter.Email + " | " + joinResult.Renter.Telephone,
                        Driver = driver.Name,
                        Start = joinResult.Contract.StartLocation + " | " + joinResult.Contract.StartDateTime.ToString(),
                        End = joinResult.Contract.EndLocation + " | " + joinResult.Contract.EndDateTime.ToString(),
                        TotalAmount = joinResult.Contract.TotalAmount,
                        Deposit = joinResult.Contract.Deposit
                    })
                .ToList();
            return View(contractsList);
        }
        [HttpGet("/[controller]/[action]/{id}")]
        public IActionResult Details(int id)
        {
            using var context = new RentACarContext();
            var contractDetails = context.Contracts
            .Where(r => r.Id == id)
                .Join(
                    context.Cars,
                    contract => contract.CarId,
                    car => car.Id,
                    (contract, car) => new
                    {
                        Contract = contract,
                        Car = car
                    })
                .Join(
                    context.Customers,
                    joinResult => joinResult.Contract.RenterId,
                    renter => renter.Id,
                    (joinResult, renter) => new
                    {
                        joinResult.Contract,
                        joinResult.Car,
                        Renter = renter
                    })
                .Join(
                    context.Customers,
                    joinResult => joinResult.Contract.DriverId,
                    driver => driver.Id,
                    (joinResult, driver) => new ContractDto
                    {
                        Id = joinResult.Contract.Id,
                        Car = joinResult.Car.Make + " " + joinResult.Car.Model + " (" + joinResult.Car.RegistrationPlate + ")",
                        Renter = joinResult.Renter.Name,
                        Contact = joinResult.Renter.Email + " | " + joinResult.Renter.Telephone,
                        Driver = driver.Name,
                        Start = joinResult.Contract.StartLocation + " | " + joinResult.Contract.StartDateTime.ToString(),
                        End = joinResult.Contract.EndLocation + " | " + joinResult.Contract.EndDateTime.ToString(),
                        TotalAmount = joinResult.Contract.TotalAmount,
                        Deposit = joinResult.Contract.Deposit
                    })
            .FirstOrDefault();
            return View("Details", contractDetails);
        }
        [HttpGet("/[controller]/[action]/{id}")]
        public IActionResult Edit(int id)
        {
            using var context = new RentACarContext();
            var carDropdown = context.Cars
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Make + " " + c.Model + " - " + c.RegistrationPlate
            })
            .ToList();
            var customerDropdown = context.Customers
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
            .ToList();

            ViewBag.CarDropdown = carDropdown;
            ViewBag.CustomerDropdown = customerDropdown;
            var contractToBeUpdated = context.Contracts.Find(id);
            return View(contractToBeUpdated);
        }
        [HttpPost("/[controller]/[action]/{id}")]
        public IActionResult Edit(Contract editedContract)
        {
            using var context = new RentACarContext();
            if (ModelState.IsValid)
            {
                context.Contracts.Update(editedContract);
                context.SaveChanges();
                return RedirectToAction("Details", new { id = editedContract.Id });
            }
            return View(editedContract);
        }
        [HttpGet("/[controller]/[action]")]
        public IActionResult Create()
        {
            using var context = new RentACarContext();
            var carDropdown = context.Cars
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Make + " " + c.Model + " - " + c.RegistrationPlate
            })
            .ToList();
            var customerDropdown = context.Customers
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
            .ToList();

            ViewBag.CarDropdown = carDropdown;
            ViewBag.CustomerDropdown = customerDropdown;
            return View();
        }
        [HttpPost("/[controller]/[action]")]
        public IActionResult Create(Contract createdContract)
        {
            using var context = new RentACarContext();
            // if (ModelState.IsValid)
            // {
            context.Contracts.Add(createdContract);
            context.SaveChanges();
            return RedirectToAction("Details", new { id = createdContract.Id });
            // }
            // return View(createdContract);
        }
        [HttpPost("/[controller]/[action]/{id}")]
        public IActionResult Delete(int id)
        {
            using var context = new RentACarContext();
            var contractToDelete = context.Contracts.Find(id);
            context.Contracts.Remove(contractToDelete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}