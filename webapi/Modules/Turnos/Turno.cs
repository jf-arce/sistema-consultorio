using webapi.Modules.Doctores;
using webapi.Modules.Pacientes;
using webapi.Modules.Turnos.Enums;
using webapi.Shared;

namespace webapi.Modules.Turnos;

public class Turno : BaseEntity, ISoftDelete
{
    public DateTime Fecha { get; set; }
    public TurnoEstado Estado { get; set; } = TurnoEstado.Pendiente;
    public DateTime? DeletedAt { get; set; }

    public Guid PacienteId { get; set; }
    public Paciente Paciente { get; set; } = null!;

    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
}
