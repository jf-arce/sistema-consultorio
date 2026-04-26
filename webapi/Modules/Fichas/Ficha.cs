using webapi.Modules.Consultas;
using webapi.Modules.Doctores;
using webapi.Modules.Pacientes;

namespace webapi.Modules.Fichas;

public class Ficha
{
    public int Id { get; set; }
    public DateOnly FechaCreacion { get; set; }

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = null!;
    public int PacienteId { get; set; }
    public Paciente Paciente { get; set; } = null!;
    public ICollection<Consulta> Consultas { get; set; } = null!;
}
