using CarAvailabilityService.Configs;
using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace CarAvailabilityService.Kafka
{
    public class BookingConsumer : BackgroundService
    {
        private readonly ILogger<BookingConsumer> _logger;
        private readonly IConsumer<Null, string> _consumer;

        public BookingConsumer(IOptions<KafkaSettings> kafkaSettings, ILogger<BookingConsumer> logger)
        {
            _logger = logger;
            var settings = kafkaSettings.Value;

            var config = new ConsumerConfig
            {
                BootstrapServers = settings.BootstrapServers,
                GroupId = settings.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Null, string>(config).Build();
            _consumer.Subscribe(settings.Topic);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Kafka Consumer запущен...");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);
                    var message = consumeResult.Message.Value;

                    _logger.LogInformation("Получено сообщение из Kafka: {Message}", message);

                    // Здесь можно добавить обработку: например, проверку доступности машины
                    await ProcessMessage(message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при обработке сообщения Kafka");
                }
            }
        }

        private Task ProcessMessage(string message)
        {
            _logger.LogInformation("✅ Сообщение обработано: {Message}", message);
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
            base.Dispose();
        }
    }
}
