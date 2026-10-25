
using Microsoft.EntityFrameworkCore;
using TransportTerminal.Domain.Entities;

namespace TransportTerminal.Infrastructure.Persistence;

public sealed class TerminalDbContext(
    DbContextOptions<TerminalDbContext> options) : DbContext(options)
{
    public DbSet<Terminal> Terminals => Set<Terminal>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<TerminalCompany> TerminalCompanies => Set<TerminalCompany>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Route> Routes => Set<Route>();
    public DbSet<Bay> Bays => Set<Bay>();
    public DbSet<Dispatch> Dispatches => Set<Dispatch>();
    public DbSet<VehicleMovement> VehicleMovements => Set<VehicleMovement>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Terminal>(e =>
        {
            e.ToTable("terminals");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(160).IsRequired();
            e.Property(x => x.Code).HasMaxLength(32).IsRequired();
            e.HasIndex(x => x.Code).IsUnique();
        });

        b.Entity<Company>(e =>
        {
            e.ToTable("companies");
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(200).IsRequired();
            e.Property(x => x.Nit).HasMaxLength(32).IsRequired();
            e.HasIndex(x => x.Nit).IsUnique();
        });

        b.Entity<TerminalCompany>(e =>
        {
            e.ToTable("terminal_companies");
            e.HasKey(x => new { x.TerminalId, x.CompanyId });

            e.HasOne<Terminal>()
                .WithMany()
                .HasForeignKey(x => x.TerminalId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne<Company>()
                .WithMany()
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        b.Entity<Vehicle>(e =>
        {
            e.ToTable("vehicles", t =>
                t.HasCheckConstraint(
                    "ck_vehicles_capacity_positive",
                    "capacity > 0"));

            e.HasKey(x => x.Id);
            e.Property(x => x.Plate).HasMaxLength(16).IsRequired();
            e.Property(x => x.Type).HasConversion<string>().HasMaxLength(24);
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(24);

            e.HasIndex(x => new { x.TerminalId, x.Plate }).IsUnique();

            e.HasOne<Terminal>()
                .WithMany()
                .HasForeignKey(x => x.TerminalId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne<TerminalCompany>()
                .WithMany()
                .HasForeignKey(x => new { x.TerminalId, x.CompanyId })
                .HasPrincipalKey(x => new { x.TerminalId, x.CompanyId })
                .OnDelete(DeleteBehavior.Restrict);
        });

        b.Entity<Route>(e =>
        {
            e.ToTable("routes", t =>
                t.HasCheckConstraint(
                    "ck_routes_estimated_minutes_positive",
                    "estimated_minutes > 0"));

            e.HasKey(x => x.Id);
            e.Property(x => x.Origin).HasMaxLength(160).IsRequired();
            e.Property(x => x.Destination).HasMaxLength(160).IsRequired();

            e.HasOne<Terminal>()
                .WithMany()
                .HasForeignKey(x => x.TerminalId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        b.Entity<Bay>(e =>
        {
            e.ToTable("bays");
            e.HasKey(x => x.Id);
            e.Property(x => x.Code).HasMaxLength(32).IsRequired();
            e.HasIndex(x => new { x.TerminalId, x.Code }).IsUnique();

            e.HasOne<Terminal>()
                .WithMany()
                .HasForeignKey(x => x.TerminalId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        b.Entity<Dispatch>(e =>
        {
            e.ToTable("dispatches");
            e.HasKey(x => x.Id);
            e.Property(x => x.Status).HasConversion<string>().HasMaxLength(24);

            e.HasOne<Terminal>()
                .WithMany()
                .HasForeignKey(x => x.TerminalId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne<Vehicle>()
                .WithMany()
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne<Route>()
                .WithMany()
                .HasForeignKey(x => x.RouteId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne<Bay>()
                .WithMany()
                .HasForeignKey(x => x.BayId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        b.Entity<VehicleMovement>(e =>
        {
            e.ToTable("vehicle_movements");
            e.HasKey(x => x.Id);
            e.Property(x => x.Type).HasConversion<string>().HasMaxLength(16);

            e.HasOne<Terminal>()
                .WithMany()
                .HasForeignKey(x => x.TerminalId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne<Vehicle>()
                .WithMany()
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(x => new
            {
                x.TerminalId,
                x.VehicleId,
                x.Timestamp
            });
        });
    }
}
