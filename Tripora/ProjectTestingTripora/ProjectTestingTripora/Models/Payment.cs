namespace Tripora.Models;

public class Payment
{
    public int PaymentId { get; set; }

    public int BookingId { get; set; }
    public Booking? Booking { get; set; }

    public string PaymentMethod { get; set; } = "";
    public DateTime PaymentDate { get; set; } = DateTime.Now;
    public decimal Amount { get; set; }

    public string PaymentStatus { get; set; } = "Paid"; // Paid/Failed/Refund
}
