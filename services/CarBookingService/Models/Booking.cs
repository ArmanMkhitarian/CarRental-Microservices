namespace CarBookingService.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CarModel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
    }
}
