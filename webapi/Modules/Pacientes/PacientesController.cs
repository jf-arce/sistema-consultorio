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
    public async Task<ActionResult<PacienteResponseDto>> Create(PacienteCreateDto pacienteCreateDto)
    {
        var paciente = await _pacienteService.Create(pacienteCreateDto);
        return CreatedAtAction(nameof(GetById), new { id = paciente.Id }, paciente);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PacienteResponseDto>>> GetAll()
    {
        var pacientes = await _pacienteService.GetAll();
        return Ok(pacientes);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PacienteResponseDto>> GetById(Guid id)
    {
        var paciente = await _pacienteService.GetById(id);
        return Ok(paciente);
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<PacienteResponseDto>> Update(Guid id, PacienteUpdateDto pacienteUpdateDto)
    {
        var paciente = await _pacienteService.Update(id, pacienteUpdateDto);
        return Ok(paciente);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _pacienteService.Delete(id);
        return NoContent();
    }
}
