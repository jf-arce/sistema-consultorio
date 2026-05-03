using webapi.Modules.Consultas;
using webapi.Modules.Doctores;
using webapi.Modules.Pacientes;
using webapi.Shared;

namespace webapi.Modules.Fichas;

public class Ficha : BaseEntity, ISoftDelete
{
    public DateTime? DeletedAt { get; set; }

    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public Guid PacienteId { get; set; }
    public Paciente Paciente { get; set; } = null!;
    public ICollection<Consulta> Consultas { get; set; } = null!;
}
