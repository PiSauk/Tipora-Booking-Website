namespace Tripora.Models;

public class Transport
{
    public int TransportId { get; set; }

    public int OperatorId { get; set; }
    public Operator? Operator { get; set; }

    public string? TransportType { get; set; }
    public string BusNumber { get; set; } = "";
    public int Capacity { get; set; }

    public List<Schedule> Schedules { get; set; } = new();
}
