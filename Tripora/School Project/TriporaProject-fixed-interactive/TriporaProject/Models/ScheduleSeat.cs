using System;
using System.Collections.Generic;

namespace TriporaProject.Models;

public partial class ScheduleSeat
{
    public int ScheduleseatId { get; set; }

    public int ScheduleId { get; set; }

    public string SeatCode { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int? HeldByUserId { get; set; }

    public DateTime? HoldExpiry { get; set; }

    public decimal? Price { get; set; }

    public virtual User? HeldByUser { get; set; }

    public virtual Schedule Schedule { get; set; } = null!;
}
