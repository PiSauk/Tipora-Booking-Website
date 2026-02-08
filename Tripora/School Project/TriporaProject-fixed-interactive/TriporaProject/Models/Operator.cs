using System;
using System.Collections.Generic;

namespace TriporaProject.Models;

public partial class Operator
{
    public int OperatorId { get; set; }

    public string OperatorName { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}
