using CarBookingService.Models;

namespace CarBookingService.Services
{
    public interface IBookingService
    {
        Task<Booking> CreateBookingAsync(Booking booking);
    }
}
