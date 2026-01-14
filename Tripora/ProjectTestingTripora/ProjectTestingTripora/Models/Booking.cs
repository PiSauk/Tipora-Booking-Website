using Microsoft.AspNetCore.Identity;

namespace Tripora.Models;

public class Booking
{
    public int BookingId { get; set; }

    // FK to Identity user
    public string UserId { get; set; } = "";
    public IdentityUser? User { get; set; }

    public int ScheduleId { get; set; }
    public Schedule? Schedule { get; set; }

    public DateTime BookingDate { get; set; } = DateTime.Now;
    public int NumSeats { get; set; }
    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = "Pending"; // Pending/Confirm/Cancelled

    public List<Payment> Payments { get; set; } = new();
}
