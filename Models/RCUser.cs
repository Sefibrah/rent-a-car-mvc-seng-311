using Microsoft.AspNetCore.Identity;
namespace rent_a_car_mvc.Models;
public class RCUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
}