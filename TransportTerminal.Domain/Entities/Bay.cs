namespace TransportTerminal.Domain.Entities;
public sealed class Bay
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TerminalId { get; set; }
    public string Code { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
