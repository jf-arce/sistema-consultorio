using webapi.Enums;

namespace webapi.Models;

public class Turno
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public TurnoEstado Estado { get; set; } = TurnoEstado.Pendiente;

    public int PacienteId { get; set; }
    public Paciente Paciente { get; set; } = null!;

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
}
