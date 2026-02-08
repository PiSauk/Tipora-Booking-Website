namespace TriporaProject.Models;

public class TripResult
{
    public int TripId { get; set; }
    public string OperatorName { get; set; } = "";
    public string From { get; set; } = "";
    public string To { get; set; } = "";
    public TimeOnly DepartTime { get; set; }
    public TimeOnly ArriveTime { get; set; }
    public decimal Price { get; set; }
    public int SeatsLeft { get; set; }
    public string PickupPoint { get; set; } = "";
    public string DropoffPoint { get; set; } = "";

    public string DurationText
    {
        get
        {
            var depart = DepartTime.ToTimeSpan();
            var arrive = ArriveTime.ToTimeSpan();
            var dur = arrive - depart;
            if (dur < TimeSpan.Zero) dur += TimeSpan.FromDays(1);
            return $"{(int)dur.TotalHours}h {dur.Minutes}m";
        }
    }
}
