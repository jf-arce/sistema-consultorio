using webapi.Modules.Fichas;
using webapi.Modules.Turnos;
using webapi.Shared;

namespace webapi.Modules.Pacientes;

public class Paciente : BaseEntity, ISoftDelete
{
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public int Dni { get; set; }
    public DateOnly FechaNacimiento { get; set; }
    public string? Email { get; set; } = null!;
    public string? Telefono { get; set; } = null!;
    public string? Direccion { get; set; } = null!;
    public DateTime? DeletedAt { get; set; }

    public Ficha Ficha { get; set; } = null!;
    public ICollection<Turno> Turnos { get; set; } = null!;
}
