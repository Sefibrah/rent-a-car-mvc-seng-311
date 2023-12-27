namespace rent_a_car_mvc.Dtos
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public string Car { get; set; }
        public string Renter { get; set; }
        public string Contact { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string FlightNumber { get; set; }
        public double TotalAmount { get; set; }
        public double Deposit { get; set; }
    }
}