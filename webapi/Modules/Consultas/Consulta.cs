using webapi.Modules.Fichas;
using webapi.Shared;

namespace webapi.Modules.Consultas;

public class Consulta : BaseEntity, ISoftDelete
{
    public DateTime Fecha { get; set; }
    public string? Motivo { get; set; } = null!;
    public string? Diagnostico { get; set; }
    public string? Tratamiento { get; set; }
    public string? Observaciones { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Guid FichaId { get; set; }
    public Ficha Ficha { get; set; } = null!;
}
