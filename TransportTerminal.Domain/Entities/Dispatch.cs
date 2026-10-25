namespace TransportTerminal.Domain.Entities;
public enum DispatchStatus { Scheduled, Boarding, Departed, Cancelled, Delayed }
public sealed class Dispatch
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TerminalId { get; set; }
    public Guid VehicleId { get; set; }
    public Guid RouteId { get; set; }
    public Guid BayId { get; set; }
    public DateTimeOffset ScheduledDeparture { get; set; }
    public DateTimeOffset? ActualDeparture { get; set; }
    public DispatchStatus Status { get; set; } = DispatchStatus.Scheduled;
}
