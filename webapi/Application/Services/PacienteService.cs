using System.Net;
using Microsoft.EntityFrameworkCore;
using webapi.Application.DTOs;
using webapi.Application.Interfaces;
using webapi.Data;
using webapi.Exceptions;
using webapi.Models;

namespace webapi.Application.Services;

public class PacienteService : IPacienteService
{
    private readonly AppDbContext _db;

    public PacienteService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Paciente> Create(CreatePacienteDto createPacienteDto)
    {
        var paciente = new Paciente
        {
            Nombre = createPacienteDto.Nombre,
            Apellido = createPacienteDto.Apellido,
            Dni = createPacienteDto.Dni,
            FechaNacimiento = createPacienteDto.FechaNacimiento,
            Email = createPacienteDto.Email,
            Telefono = createPacienteDto.Telefono,
            Direccion = createPacienteDto.Direccion
        };

        _db.Pacientes.Add(paciente);
        await _db.SaveChangesAsync();

        return paciente;
    }

    public async Task<IEnumerable<Paciente>> GetAll()
    {
        var pacientes = await _db.Pacientes.ToListAsync();

        if (pacientes == null || pacientes.Count == 0)
        {
            throw new CustomException(HttpStatusCode.NotFound, "No se encontraron pacientes.");
        }

        var pacientesDto = pacientes.Select(p => new Paciente
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Apellido = p.Apellido,
            Dni = p.Dni,
            FechaNacimiento = p.FechaNacimiento,
            Email = p.Email,
            Telefono = p.Telefono,
            Direccion = p.Direccion
        });

        return pacientesDto;
    }

    public async Task<Paciente> GetById(int id)
    {
        var paciente = await _db.Pacientes.FindAsync(id);

        if (paciente == null)
        {
            throw new CustomException(HttpStatusCode.NotFound, $"Paciente con ID {id} no encontrado.");
        }

        return paciente;
    }
}
