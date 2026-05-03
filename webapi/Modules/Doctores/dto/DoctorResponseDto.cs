using webapi.Shared.dto;

namespace webapi.Modules.Doctores.dto;

public class DoctorResponseDto : BaseDto
{
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Especialidad { get; set; } = null!;
}
