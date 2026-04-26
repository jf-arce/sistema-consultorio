namespace webapi.Modules.Pacientes.Dto;

public class UpdatePacienteDto
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public int? Dni { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public string? Email { get; set; } 
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }

}