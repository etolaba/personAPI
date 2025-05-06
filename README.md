# PersonApi (.NET 8)

This solution implements a basic ASP.NET Core Web API using the CQRS pattern with MediatR.

## Projects

- `PersonApi.API`: Web API project using [ApiController].
- `PersonApi.Application`: Command/query handlers, DTOs, validation (FluentValidation).
- `PersonApi.Domain`: Entities, enums, domain interfaces.
- `PersonApi.Infrastructure`: In-memory data storage, Serilog logging.

## Features

- .NET 8 + Top-level statements
- CQRS with MediatR
- Validation with FluentValidation
- Logging with Serilog
- Swagger UI enabled
- Clean architecture with multiple projects

## How to Run

1. Open the solution in Visual Studio or VS Code.
2. Restore NuGet packages.
3. Set `PersonApi.API` as the startup project.
4. Run the app and navigate to `/swagger`.

## Requirements

- .NET 8 SDK
