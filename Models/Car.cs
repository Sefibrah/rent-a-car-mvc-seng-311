using System.ComponentModel.DataAnnotations;
namespace rent_a_car_mvc.Models;
public class Car
{
    public int Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string RegistrationPlate { get; set; }
    public List<Contract>? Contracts { get; set; }
    public List<Reservation>? Reservations { get; set; }
}