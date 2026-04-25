using System.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Application.Interfaces;
using webapi.Models;

namespace webapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetAll()
        {
            var pacientes = await _pacienteService.GetAll();
            return Ok(pacientes);
        }
    }
}
