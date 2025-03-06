using CarBookingService.Models;
using CarBookingService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarBookingService.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Создать новое бронирование автомобиля
        /// </summary>
        /// <param name="booking">Данные бронирования</param>
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
        {
            if (booking == null)
            {
                return BadRequest("Данные бронирования отсутствуют.");
            }

            var createdBooking = await _bookingService.CreateBookingAsync(booking);
            return Ok(createdBooking);
        }

        /// <summary>
        /// Получить тестовый ответ (для проверки работы API)
        /// </summary>
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("CarBookingService API is running");
        }
    }
}
