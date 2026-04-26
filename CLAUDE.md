# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Rules

- No vuelvas a leer archivos ya leídos en esta sesión a menos que te lo pida. Minimiza las llamadas a herramientas y trabaja con lo que ya tienes en contexto.
- Si necesitas información adicional, haz preguntas específicas para que te la proporcione.

## Commands

All commands run from the `webapi/` directory unless noted.

```bash
# Build
dotnet build

# Run (starts on http://localhost:5205)
dotnet run

# EF Core migrations (run from webapi/)
dotnet ef migrations add <MigrationName> --output-dir Data/Migrations
dotnet ef database update
dotnet ef migrations remove

# Reset database
rm app.db && dotnet ef database update
```

There are no automated tests. Use `webapi/webapi.http` or Swagger (`/openapi/v1.json`) for manual testing.

## Architecture

ASP.NET Core 10 Web API using **feature-based (vertical slice) organization**. Each domain concept lives in `Modules/<Name>/` with its own controller, service interface, service implementation, DTOs, and validators.

**Stack:**
- Entity Framework Core 10 + SQLite (`app.db`)
- FluentValidation 12 — validators auto-discovered from assembly
- Mapster 10 — DTO ↔ entity mapping (use `.ProjectToType<T>()` on queries)
- `CustomException(HttpStatusCode, message, errors?)` — caught and serialized by `ExceptionHandlingMiddleware`

**Base types in `Shared/`:**
- `BaseEntity` — provides `Id`, `CreatedAt`, `UpdatedAt` (set automatically in `AppDbContext.SaveChangesAsync`)
- `ISoftDelete` — adds `DeletedAt`; `AppDbContext` registers global query filters to exclude soft-deleted records

**DI registration:** each module's service is registered in `Program.cs` as scoped: `AddScoped<IFooService, FooService>()`.

**Relationships:**
```
Paciente (1)→(1) Ficha
Paciente (1)→(M) Turno
Ficha    (1)→(M) Consulta
Doctor   (1)→(M) Turno
Doctor   (1)→(M) Ficha
```

## Module conventions

When adding a new module, follow the Pacientes module as the reference implementation:

1. Entity inherits `BaseEntity`, implements `ISoftDelete`
2. Controller: `[ApiController]`, `[Route("api/[controller]")]`, inject service via constructor
3. Service interface + implementation — inject `AppDbContext` directly (no repository layer)
4. DTOs in `Dto/` (`CreateDto`, `UpdateDto`, `ResponseDto`)
5. Validators in `Validators/` inheriting `AbstractValidator<TDto>`, written in Spanish
6. Add `DbSet<T>` to `AppDbContext` and configure relationships in `OnModelCreating`
7. Register service in `Program.cs`

## Notes

- Domain terminology and validation messages are in Spanish.
- Connection string is hardcoded in `Program.cs` (no `appsettings` entry).
- Authentication/authorization middleware is wired but not implemented.
- Only Pacientes is fully built out; other modules (Doctores, Turnos, Fichas, Consultas) have entity classes only.
