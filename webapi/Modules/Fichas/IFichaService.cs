using webapi.Modules.Fichas.Dto;

namespace webapi.Modules.Fichas;

public interface IFichaService
{
    Task<FichaResponseDto> Create(FichaCreateDto fichaCreateDto);
    Task<FichaResponseDto> GetByPacienteId(Guid pacienteId);
}
