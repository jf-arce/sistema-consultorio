using webapi.Modules.Fichas;
using webapi.Modules.Turnos;
using webapi.Shared;

namespace webapi.Modules.Doctores;

public class Doctor : BaseEntity, ISoftDelete
{
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Especialidad { get; set; } = null!;
    public DateTime? DeletedAt { get; set; }

    public ICollection<Turno> Turnos { get; set; } = null!;
    public ICollection<Ficha> Fichas { get; set; } = null!;

    public string NombreCompleto()
    {
        return $"{Nombre} {Apellido}";
    }
}
