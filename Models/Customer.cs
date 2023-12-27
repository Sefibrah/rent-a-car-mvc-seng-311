using System.ComponentModel.DataAnnotations;
namespace rent_a_car_mvc.Models;
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [DataType(DataType.PhoneNumber)]
    public string Telephone { get; set; }
    public List<Contract>? Contracts { get; set; }
    public List<Reservation>? Reservations { get; set; }
}