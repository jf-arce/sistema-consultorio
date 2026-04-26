using webapi.Modules.Pacientes.Dto;

namespace webapi.Modules.Pacientes;

public interface IPacienteService
{
    Task<PacienteResponseDto> Create(PacienteCreateDto pacienteCreateDto);
    Task<IEnumerable<PacienteResponseDto>> GetAll();
    Task<PacienteResponseDto> GetById(int id);
    Task<PacienteResponseDto> Update(int id, PacienteUpdateDto pacienteUpdateDto);
    Task Delete(int id);
}
