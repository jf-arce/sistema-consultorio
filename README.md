# Sistema Consultório Médico

API REST en ASP.NET Core 10 para gestión de un consultorio médico (pacientes, doctores, turnos, fichas y consultas).

## Arquitectura

El proyecto usa **Feature-Based Architecture**. Cada entidad tiene su propio módulo dentro de `Modules/`, agrupando en un mismo lugar el controller, service, interface, entidad, DTOs y validators. Lo compartido entre módulos (excepciones, middlewares) vive fuera en carpetas propias (`Shared/`, `Middlewares/`). Las extensions de configuración de ASP.NET van en `Extensions/`. La capa de datos (DbContext y Migrations) vive en `Data/`.

## Migraciones

Las migrations viven en `Data/Migrations/`. Hay que indicar el output dir en cada comando.

**Crear una nueva migración**
```bash
dotnet ef migrations add NombreDeLaMigracion --output-dir Data/Migrations
```

**Aplicar migraciones a la base de datos**
```bash
dotnet ef database update
```

**Revertir la última migración**
```bash
dotnet ef migrations remove
```
