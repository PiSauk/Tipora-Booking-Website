using System;
using System.Collections.Generic;

namespace TriporaProject.Models;

public partial class BookingSeat
{
    public int BookingSeatId { get; set; }

    public int BookingId { get; set; }

    public int ScheduleId { get; set; }

    public string SeatCode { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Schedule Schedule { get; set; } = null!;
}
