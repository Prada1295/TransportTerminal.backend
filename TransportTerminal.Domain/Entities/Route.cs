namespace TransportTerminal.Domain.Entities;
public sealed class Route
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TerminalId { get; set; }
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public int EstimatedMinutes { get; set; }
    public bool IsActive { get; set; } = true;
}
