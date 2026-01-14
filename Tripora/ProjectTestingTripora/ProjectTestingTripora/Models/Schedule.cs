namespace Tripora.Models;

public class Schedule
{
    public int ScheduleId { get; set; }

    public int TransportId { get; set; }
    public Transport? Transport { get; set; }

    public int? RouteId { get; set; } // optional for now

    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }

    public decimal BasePrice { get; set; }
    public int AvailableSeats { get; set; }

    public List<Booking> Bookings { get; set; } = new();
}
