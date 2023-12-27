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
    public class ReservationController : Controller
    {
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ILogger<ReservationController> logger)
        {
            _logger = logger;
        }
        [HttpGet("/[controller]/[action]")]
        public IActionResult Index()
        {
            using var context = new RentACarContext();
            var reservationsList = context.Reservations
                .Join(
                    context.Customers,
                    reservation => reservation.RenterId,
                    customer => customer.Id,
                    (reservation, customer) => new
                    {
                        Reservation = reservation,
                        Customer = customer
                    })
                .Join(
                    context.Cars,
                    joinResult => joinResult.Reservation.CarId,
                    car => car.Id,
                    (joinResult, car) => new ReservationDto
                    {
                        Id = joinResult.Reservation.Id,
                        Car = car.Make + " " + car.Model + " (" + car.RegistrationPlate + ")",
                        Renter = joinResult.Customer.Name,
                        Contact = joinResult.Customer.Email + " | " + joinResult.Customer.Telephone,
                        Start = joinResult.Reservation.StartDateTime.ToLocalTime() + " | " + joinResult.Reservation.StartLocation,
                        End = joinResult.Reservation.EndDateTime.ToLocalTime() + " | " + joinResult.Reservation.EndLocation,
                        FlightNumber = joinResult.Reservation.FlightNumber,
                        Deposit = joinResult.Reservation.Deposit,
                        TotalAmount = joinResult.Reservation.TotalAmount,
                    })
                .ToList();
            return View(reservationsList);
        }
        [HttpGet("/[controller]/[action]/{id}")]
        public IActionResult Details(int id)
        {
            using var context = new RentACarContext();
            var reservationDetails = context.Reservations
            .Where(r => r.Id == id)
            .Join(
                context.Customers,
                reservation => reservation.RenterId,
                customer => customer.Id,
                (reservation, customer) => new
                {
                    Reservation = reservation,
                    Customer = customer
                })
            .Join(
                context.Cars,
                joinResult => joinResult.Reservation.CarId,
                car => car.Id,
                (joinResult, car) => new ReservationDto
                {
                    Id = joinResult.Reservation.Id,
                    Car = car.Make + " " + car.Model + " (" + car.RegistrationPlate + ")",
                    Renter = joinResult.Customer.Name,
                    Contact = joinResult.Customer.Email + " | " + joinResult.Customer.Telephone,
                    Start = joinResult.Reservation.StartLocation + " | " + joinResult.Reservation.StartDateTime.ToString(),
                    End = joinResult.Reservation.EndLocation + " | " + joinResult.Reservation.EndDateTime.ToString(),
                    FlightNumber = joinResult.Reservation.FlightNumber,
                    Deposit = joinResult.Reservation.Deposit,
                    TotalAmount = joinResult.Reservation.TotalAmount,
                })
            .FirstOrDefault();
            return View("Details", reservationDetails);
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
            var reservationToBeUpdated = context.Reservations.Find(id);
            return View(reservationToBeUpdated);
        }
        [HttpPost("/[controller]/[action]/{id}")]
        public IActionResult Edit(Reservation editedReservation)
        {
            using var context = new RentACarContext();
            if (ModelState.IsValid)
            {
                context.Reservations.Update(editedReservation);
                context.SaveChanges();
                return RedirectToAction("Details", new { id = editedReservation.Id });
            }
            return View(editedReservation);
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
        public IActionResult Create(Reservation createdReservation)
        {
            using var context = new RentACarContext();
            if (ModelState.IsValid)
            {
                context.Reservations.Add(createdReservation);
                context.SaveChanges();
                return RedirectToAction("Details", new { id = createdReservation.Id });
            }
            return View(createdReservation);
        }
        [HttpPost("/[controller]/[action]/{id}")]
        public IActionResult Delete(int id)
        {
            using var context = new RentACarContext();
            var reservationToDelete = context.Reservations.Find(id);
            context.Reservations.Remove(reservationToDelete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}