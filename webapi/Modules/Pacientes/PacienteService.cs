using System.Net;
using Mapster;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Modules.Pacientes.Dto;
using webapi.Shared.Exceptions;

namespace webapi.Modules.Pacientes;

public class PacienteService : IPacienteService
{
    private readonly AppDbContext _db;

    public PacienteService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<PacienteResponseDto> Create(PacienteCreateDto pacienteCreateDto)
    {
        var paciente = pacienteCreateDto.Adapt<Paciente>();

        _db.Pacientes.Add(paciente);
        await _db.SaveChangesAsync();

        var pacienteDto = paciente.Adapt<PacienteResponseDto>();
        return pacienteDto;
    }

    public async Task<IEnumerable<PacienteResponseDto>> GetAll()
    {
        var pacientesDto = await _db.Pacientes.ProjectToType<PacienteResponseDto>().ToListAsync();

        if (pacientesDto == null || pacientesDto.Count == 0)
        {
            throw new CustomException(HttpStatusCode.NotFound, "No se encontraron pacientes.");
        }

        return pacientesDto;
    }

    public async Task<PacienteResponseDto> GetById(int id)
    {
        var pacienteDto = await _db.Pacientes
            .ProjectToType<PacienteResponseDto>()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pacienteDto == null)
        {
            throw new CustomException(HttpStatusCode.NotFound, $"Paciente con ID {id} no encontrado.");
        }

        return pacienteDto;
    } 

    public async Task<PacienteResponseDto> Update(int id, PacienteUpdateDto pacienteUpdateDto)
    {
        var paciente = await _db.Pacientes.FindAsync(id);

        if (paciente == null)
        {
            throw new CustomException(HttpStatusCode.NotFound, $"Paciente con ID {id} no encontrado.");
        }

        paciente.Nombre = pacienteUpdateDto.Nombre ?? paciente.Nombre;
        paciente.Apellido = pacienteUpdateDto.Apellido ?? paciente.Apellido;
        paciente.Dni = pacienteUpdateDto.Dni ?? paciente.Dni;
        paciente.FechaNacimiento = pacienteUpdateDto.FechaNacimiento ?? paciente.FechaNacimiento;
        paciente.Email = pacienteUpdateDto.Email ?? paciente.Email;
        paciente.Telefono = pacienteUpdateDto.Telefono ?? paciente.Telefono;
        paciente.Direccion = pacienteUpdateDto.Direccion ?? paciente.Direccion;

        await _db.SaveChangesAsync();

        var pacienteDto = paciente.Adapt<PacienteResponseDto>();

        return pacienteDto;
    }

    public async Task Delete(int id)
    {
        var paciente = await _db.Pacientes.FindAsync(id);

        if (paciente == null)
        {
            throw new CustomException(HttpStatusCode.NotFound, $"Paciente con ID {id} no encontrado.");
        }

        _db.Pacientes.Remove(paciente);
        await _db.SaveChangesAsync();
    }
}
