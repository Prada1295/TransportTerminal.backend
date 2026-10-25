namespace TransportTerminal.Domain.Entities;
public sealed class Company
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Nit { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
