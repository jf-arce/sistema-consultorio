# UUIDs, Timestamps y Soft Delete

## BaseEntity y ISoftDelete

Toda entidad hereda de `BaseEntity`, definida en `Shared/BaseEntity.cs`:

```csharp
public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
```

Las entidades que deben soportar eliminación lógica implementan además `ISoftDelete`:

```csharp
public interface ISoftDelete
{
    DateTime? DeletedAt { get; set; }
}
```

Actualmente todas las entidades (`Paciente`, `Doctor`, `Turno`, `Ficha`, `Consulta`) implementan ambas. Si en el futuro se agrega una entidad que no deba tener soft delete, simplemente no implementa `ISoftDelete`.

## Por qué UUIDs en lugar de int

Los IDs son `Guid` en vez de `int` autoincremental. Esto evita exponer en la API información sobre cuántos registros existen y hace los IDs no predecibles. EF Core los almacena como `TEXT` en SQLite. El valor se genera en la aplicación (no en la base de datos) mediante el inicializador `= Guid.NewGuid()` en `BaseEntity`.

## SaveChangesAsync en AppDbContext

Se sobreescribe `SaveChangesAsync` para manejar automáticamente los timestamps y el soft delete, sin que los servicios tengan que preocuparse por ello.

**Timestamps automáticos:** recorre todas las entidades que hereden `BaseEntity` y asigna `CreatedAt` y `UpdatedAt` al insertar, y solo `UpdatedAt` al modificar.

**Intercepción de deletes físicos:** recorre las entidades que implementan `ISoftDelete`. Si alguna tiene estado `Deleted` (alguien llamó a `_db.Remove()`), cambia el estado a `Modified` y asigna `DeletedAt`. Esto convierte el `DELETE` SQL en un `UPDATE`, preservando el registro.

Esta intercepción es una red de seguridad. La forma preferida de hacer un soft delete es setear `DeletedAt` directamente en el servicio, lo que deja la intención explícita en el código.

## Soft delete en los servicios

En el servicio el delete se hace así:

```csharp
paciente.DeletedAt = DateTime.UtcNow;
await _db.SaveChangesAsync();
```

En vez de `_db.Pacientes.Remove(paciente)`. El registro queda en la base de datos con `DeletedAt` seteado.

## Query filters globales

En `OnModelCreating` cada entidad con soft delete tiene un filtro global:

```csharp
paciente.HasQueryFilter(p => p.DeletedAt == null);
```

EF Core agrega automáticamente `WHERE DeletedAt IS NULL` a todas las consultas sobre esa entidad. Los registros eliminados lógicamente son invisibles por defecto en toda la aplicación. Si en algún caso se necesita acceder a ellos, se usa `.IgnoreQueryFilters()` en la query específica.

## Rutas con constraint de tipo

Los controladores usan `{id:guid}` en lugar de `{id}`:

```csharp
[HttpGet("{id:guid}")]
public async Task<ActionResult<PacienteResponseDto>> GetById(Guid id) { ... }
```

Esto hace que ASP.NET Core rechace con 404 cualquier request cuyo `id` no tenga formato UUID válido, antes de que llegue al servicio.
