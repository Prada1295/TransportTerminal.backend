# TransportTerminal Backend starter

ASP.NET Core + EF Core + PostgreSQL scaffold. This is a starter package, not yet production-ready.

## Requirements
- .NET 10 SDK
- PostgreSQL

## Build
From this folder:
```bash
dotnet restore TransportTerminal.Backend.sln
dotnet build TransportTerminal.Backend.sln
```

Copy `TransportTerminal.Api/appsettings.Development.example.json` to `appsettings.Development.json` and set a local PostgreSQL connection string. Do not commit credentials.

Critical next implementation tasks: transaction-backed vehicle entry, vehicle exit, and dispatch execution; authentication/authorization; integration tests against PostgreSQL. Do not expose operational mutation endpoints publicly before auth is configured.
