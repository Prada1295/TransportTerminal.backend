
namespace TransportTerminal.Domain.Entities;

public sealed class TerminalCompany
{
    public Guid TerminalId { get; set; }
    public Guid CompanyId { get; set; }
    public bool IsActive { get; set; } = true;
}
