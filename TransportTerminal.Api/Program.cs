using Microsoft.EntityFrameworkCore;
using TransportTerminal.Infrastructure.Persistence;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks();
var cs = builder.Configuration.GetConnectionString("TerminalDatabase");
if (!string.IsNullOrWhiteSpace(cs)) builder.Services.AddDbContext<TerminalDbContext>(o => o.UseNpgsql(cs));
var app = builder.Build();
app.UseExceptionHandler();
app.MapHealthChecks("/health");
app.MapGet("/api/v1/health", () => Results.Ok(new { status = "ok", service = "TransportTerminal.Api" }));
app.MapControllers();
app.Run();
public partial class Program { }
