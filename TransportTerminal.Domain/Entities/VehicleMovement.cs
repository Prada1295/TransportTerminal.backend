namespace TransportTerminal.Domain.Entities;
public enum MovementType { Entry, Exit }
public sealed class VehicleMovement
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TerminalId { get; set; }
    public Guid VehicleId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public MovementType Type { get; set; }
}
