using System.ComponentModel.DataAnnotations;
namespace rent_a_car_mvc.Models;
public class Reservation
{
    public int Id { get; set; }
    public string StartLocation { get; set; }
    public DateTime StartDateTime { get; set; }
    public string EndLocation { get; set; }
    public DateTime EndDateTime { get; set; }
    public int? RenterId { get; set; }
    public Customer? Renter { get; set; }
    public int? CarId { get; set; }
    public Car? Car { get; set; }
    public double TotalAmount { get; set; }
    public double Deposit { get; set; }
    public string? FlightNumber { get; set; }
}