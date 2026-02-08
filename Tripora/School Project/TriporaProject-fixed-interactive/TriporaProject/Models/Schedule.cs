using System;
using System.Collections.Generic;

namespace TriporaProject.Models;

public partial class Schedule
{
    public int ScheduleId { get; set; }

    public int TransportId { get; set; }

    public int? RouteId { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public decimal BasePrice { get; set; }

    public int AvailableSeats { get; set; }

    public virtual ICollection<BookingSeat> BookingSeats { get; set; } = new List<BookingSeat>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<ScheduleSeat> ScheduleSeats { get; set; } = new List<ScheduleSeat>();

    public virtual Transport Transport { get; set; } = null!;
}
