using webapi.Modules.Pacientes.Dto;

namespace webapi.Modules.Pacientes;

public interface IPacienteService
{
    Task<Paciente> Create(CreatePacienteDto createPacienteDto);
    Task<IEnumerable<Paciente>> GetAll();
    Task<Paciente> GetById(int id);
}
