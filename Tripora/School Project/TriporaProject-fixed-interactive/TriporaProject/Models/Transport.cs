using System;
using System.Collections.Generic;

namespace TriporaProject.Models;

public partial class Transport
{
    public int TransportId { get; set; }

    public int OperatorId { get; set; }

    public string? TransportType { get; set; }

    public string BusNumber { get; set; } = null!;

    public int Capacity { get; set; }

    public virtual Operator Operator { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<SeatLayout> SeatLayouts { get; set; } = new List<SeatLayout>();
}
