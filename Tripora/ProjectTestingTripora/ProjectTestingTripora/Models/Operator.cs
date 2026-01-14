using System.Security.Cryptography.Xml;

namespace Tripora.Models;

public class Operator
{
    public int OperatorId { get; set; }
    public string OperatorName { get; set; } = "";
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }

    public List<Transport> Transports { get; set; } = new();
}
