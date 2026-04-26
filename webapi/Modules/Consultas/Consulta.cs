using webapi.Modules.Fichas;

namespace webapi.Modules.Consultas;

public class Consulta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string? Motivo { get; set; } = null!;
    public string? Diagnostico { get; set; }
    public string? Tratamiento { get; set; }
    public string? Observaciones { get; set; }

    public int FichaId { get; set; }
    public Ficha Ficha { get; set; } = null!;
}
