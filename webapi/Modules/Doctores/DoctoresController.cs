using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Modules.Doctores.dto;

namespace webapi.Modules.Doctores
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctoresController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctoresController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreateDto doctorCreateDto)
        {
            var result = await _doctorService.Create(doctorCreateDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _doctorService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _doctorService.GetById(id);
            return Ok(result);
        }
    }
}
