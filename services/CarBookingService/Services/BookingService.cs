using CarBookingService.Kafka;
using CarBookingService.Models;

namespace CarBookingService.Services
{
    public class BookingService : IBookingService
    {
        private readonly BookingProducer _producer;

        public BookingService(BookingProducer producer)
        {
            _producer = producer;
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            booking.Id = Guid.NewGuid();
            await _producer.SendBookingAsync(booking);
            return booking;
        }
    }
}
