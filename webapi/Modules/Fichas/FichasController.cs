using Microsoft.AspNetCore.Mvc;
using webapi.Modules.Fichas.Dto;

namespace webapi.Modules.Fichas;

[ApiController]
[Route("api/[controller]")]
public class FichasController : ControllerBase
{
    private readonly IFichaService _fichaService;

    public FichasController(IFichaService fichaService)
    {
        _fichaService = fichaService;
    }

    [HttpGet("paciente/{pacienteId}")]
    public async Task<ActionResult<FichaResponseDto>> GetByPacienteId(Guid pacienteId)
    {
        var ficha = await _fichaService.GetByPacienteId(pacienteId);
        return Ok(ficha);
    }
}
