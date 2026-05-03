using System;

namespace webapi.Modules.Doctores.dto;

public class DoctorCreateDto
{
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Especialidad { get; set; } = null!;
}
