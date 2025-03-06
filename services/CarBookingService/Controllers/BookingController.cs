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
        private readonly ILogger<BookingController> _logger;
        public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        /// <summary>
        /// Создать новое бронирование автомобиля
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Booking), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Booking>> CreateBooking([FromBody] Booking booking)
        {
            if (booking == null)
            {
                _logger.LogWarning("Попытка создать бронирование без данных");
                return BadRequest("Данные бронирования отсутствуют.");
            }

            try
            {
                var createdBooking = await _bookingService.CreateBookingAsync(booking);
                return Ok(createdBooking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании бронирования");
                return StatusCode(500, "Ошибка на сервере. Попробуйте позже.");
            }
        }

        /// <summary>
        /// Получить тестовый ответ (для проверки работы API)
        /// </summary>
        [HttpGet("test")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Test()
        {
            return Ok("CarBookingService API is running");
        }
    }
}
