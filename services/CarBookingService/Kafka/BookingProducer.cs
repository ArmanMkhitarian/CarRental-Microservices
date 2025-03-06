using CarBookingService.Configs;
using CarBookingService.Kafka;
using CarBookingService.Models;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using System.Text.Json;

public class BookingProducer : IBookingProducer
{
    private readonly string _topic;
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<BookingProducer> _logger;

    public BookingProducer(IOptions<KafkaSettings> kafkaSettings, ILogger<BookingProducer> logger)
    {
        _logger = logger;
        var settings = kafkaSettings.Value;

        _topic = settings.Topic;

        var config = new ProducerConfig
        {
            BootstrapServers = settings.BootstrapServers
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
        _logger.LogInformation("Kafka Producer инициализирован. Сервер: {BootstrapServers}, Топик: {Topic}", settings.BootstrapServers, _topic);
    }

    public async Task SendBookingAsync(Booking booking)
    {
        var message = JsonSerializer.Serialize(booking);

        _logger.LogInformation("Отправка в Kafka: {Message}", message);

        await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });

        _logger.LogInformation("Сообщение отправлено в топик {Topic}", _topic);
    }
}
