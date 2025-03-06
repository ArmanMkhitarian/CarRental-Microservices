using CarBookingService.Models;
using Confluent.Kafka;
using System.Text.Json;

namespace CarBookingService.Kafka
{
    public class BookingProducer
    {
        private readonly string _topic = "car-bookings";
        private readonly IProducer<Null, string> _producer;
        public BookingProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task SendBookingAsync(Booking booking)
        {
            var message = JsonSerializer.Serialize(booking);
            await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
        }
    }
}
