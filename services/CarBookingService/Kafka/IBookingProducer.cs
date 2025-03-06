using CarBookingService.Models;
using System.Threading.Tasks;

namespace CarBookingService.Kafka
{
    public interface IBookingProducer
    {
        Task SendBookingAsync(Booking booking);
    }
}
