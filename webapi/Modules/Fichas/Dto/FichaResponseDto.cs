using webapi.Modules.Doctores.dto;
using webapi.Shared.dto;

namespace webapi.Modules.Fichas.Dto;

public class FichaResponseDto : BaseDto
{
    public DoctorResponseDto Doctor { get; set; } = null!;
}
