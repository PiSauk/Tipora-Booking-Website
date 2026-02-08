using System;
using System.Collections.Generic;

namespace TriporaProject.Models;

public partial class SeatLayout
{
    public int SeatlayoutId { get; set; }

    public int TransportId { get; set; }

    public string SeatCode { get; set; } = null!;

    public int RowNo { get; set; }

    public int ColNo { get; set; }

    public virtual Transport Transport { get; set; } = null!;
}
