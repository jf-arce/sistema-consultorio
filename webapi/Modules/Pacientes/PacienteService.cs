using System.Net;
using Mapster;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Modules.Fichas;
using webapi.Modules.Fichas.Dto;
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
        var dniExistente = await _db.Pacientes.AnyAsync(p => p.Dni == pacienteCreateDto.Dni);
        if (dniExistente)
            throw new CustomException(HttpStatusCode.Conflict, $"Ya existe un paciente con el DNI {pacienteCreateDto.Dni}.");

        var doctorExists = await _db.Doctores.AnyAsync(d => d.Id == pacienteCreateDto.DoctorId);
        if (!doctorExists)
        {
            throw new CustomException(HttpStatusCode.NotFound, $"Doctor con ID {pacienteCreateDto.DoctorId} no encontrado.");
        }

        var paciente = pacienteCreateDto.Adapt<Paciente>();

        var fichaCreateDto = new FichaCreateDto
        {
            DoctorId = pacienteCreateDto.DoctorId,
            PacienteId = paciente.Id
        };
        var ficha = fichaCreateDto.Adapt<Ficha>();

        _db.Pacientes.Add(paciente);
        _db.Fichas.Add(ficha);

        await _db.SaveChangesAsync();

        var pacienteDto = paciente.Adapt<PacienteResponseDto>();
        return pacienteDto;
    }

    public async Task<IEnumerable<PacienteResponseDto>> GetAll()
    {
        var pacientesDto = await _db.Pacientes
            .ProjectToType<PacienteResponseDto>()
            .ToListAsync();

        if (pacientesDto == null || pacientesDto.Count == 0)
        {
            throw new CustomException(HttpStatusCode.NotFound, "No se encontraron pacientes.");
        }

        return pacientesDto;
    }

    public async Task<PacienteResponseDto> GetById(Guid id)
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

    public async Task<PacienteResponseDto> Update(Guid id, PacienteUpdateDto pacienteUpdateDto)
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

    public async Task Delete(Guid id)
    {
        var paciente = await _db.Pacientes.FindAsync(id);

        if (paciente == null)
        {
            throw new CustomException(HttpStatusCode.NotFound, $"Paciente con ID {id} no encontrado.");
        }

        paciente.DeletedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
    }
}
