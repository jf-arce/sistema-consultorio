namespace webapi.Modules.Pacientes.Dto;

public class PacienteResponseDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public int Dni { get; set; }
    public DateOnly FechaNacimiento { get; set; }
    public string Email { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string Direccion { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
