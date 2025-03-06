namespace CarAvailabilityService.Configs
{
    public class KafkaSettings
    {
        public string BootstrapServers { get; set; } = "localhost:9092";
        public string Topic { get; set; } = "car-bookings";
        public string GroupId { get; set; } = "car-availability-group";
    }
}
