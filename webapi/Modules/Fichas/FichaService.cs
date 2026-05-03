using System.Net;
using Mapster;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Modules.Fichas.Dto;
using webapi.Shared.Exceptions;

namespace webapi.Modules.Fichas;

public class FichaService : IFichaService
{
    private readonly AppDbContext _db;
    
    public FichaService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<FichaResponseDto> Create(FichaCreateDto fichaCreateDto)
    {
        var doctorExists = await _db.Doctores.AnyAsync(d => d.Id == fichaCreateDto.DoctorId);
        var pacienteExists = await _db.Pacientes.AnyAsync(p => p.Id == fichaCreateDto.PacienteId);
      
        if (!doctorExists)
        {
            throw new Exception($"Doctor con ID {fichaCreateDto.DoctorId} no encontrado.");
        }
        if (!pacienteExists)
        {
            throw new Exception($"Paciente con ID {fichaCreateDto.PacienteId} no encontrado.");
        }

        var ficha = fichaCreateDto.Adapt<Ficha>();
        
        _db.Fichas.Add(ficha);
        
        await _db.SaveChangesAsync();

        return ficha.Adapt<FichaResponseDto>();
    }

    public async Task<FichaResponseDto> GetByPacienteId(Guid pacienteId)
    {
        var ficha = await _db.Fichas
            .Include(f => f.Doctor)
            .FirstOrDefaultAsync(f => f.PacienteId == pacienteId);

        if (ficha == null)
            throw new CustomException(HttpStatusCode.NotFound, $"No se encontró ficha para el paciente con ID {pacienteId}.");

        return ficha.Adapt<FichaResponseDto>();
    }
}
