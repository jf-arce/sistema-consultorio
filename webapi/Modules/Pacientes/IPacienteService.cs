using webapi.Modules.Pacientes.Dto;

namespace webapi.Modules.Pacientes;

public interface IPacienteService
{
    Task<PacienteResponseDto> Create(PacienteCreateDto pacienteCreateDto);
    Task<IEnumerable<PacienteResponseDto>> GetAll();
    Task<PacienteResponseDto> GetById(Guid id);
    Task<PacienteResponseDto> Update(Guid id, PacienteUpdateDto pacienteUpdateDto);
    Task Delete(Guid id);
}
