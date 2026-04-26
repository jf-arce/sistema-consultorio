using Microsoft.AspNetCore.Mvc;
using webapi.Modules.Pacientes.Dto;

namespace webapi.Modules.Pacientes;

[Route("api/[controller]")]
[ApiController]
public class PacientesController : ControllerBase
{
    private readonly IPacienteService _pacienteService;

    public PacientesController(IPacienteService pacienteService)
    {
        _pacienteService = pacienteService;
    }

    [HttpPost]
    public async Task<ActionResult<Paciente>> Create(CreatePacienteDto createPacienteDto)
    {
        var paciente = await _pacienteService.Create(createPacienteDto);
        return CreatedAtAction(nameof(GetById), new { id = paciente.Id }, paciente);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Paciente>>> GetAll()
    {
        var pacientes = await _pacienteService.GetAll();
        return Ok(pacientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Paciente>> GetById(int id)
    {
        var paciente = await _pacienteService.GetById(id);
        return Ok(paciente);
    }
}
