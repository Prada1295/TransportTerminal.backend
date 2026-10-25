namespace TransportTerminal.Domain.Entities;
public enum VehicleStatus { OutsideTerminal, InsideTerminal, Dispatched, Maintenance }
public enum VehicleType { Bus, Buseta, Microbus, Van, Taxi }
public sealed class Vehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TerminalId { get; set; }
    public Guid CompanyId { get; set; }
    public string Plate { get; set; } = string.Empty;
    public VehicleType Type { get; set; }
    public int Capacity { get; set; }
    public VehicleStatus Status { get; set; } = VehicleStatus.OutsideTerminal;
}
