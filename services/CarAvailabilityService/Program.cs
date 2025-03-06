using CarAvailabilityService.Kafka;
using Serilog;
using CarAvailabilityService.Configs;

var builder = Host.CreateDefaultBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.UseSerilog();

builder.ConfigureServices((hostContext, services) =>
{
    services.Configure<KafkaSettings>(hostContext.Configuration.GetSection("Kafka"));
    services.AddHostedService<BookingConsumer>();
});

var app = builder.Build();
app.Run();
