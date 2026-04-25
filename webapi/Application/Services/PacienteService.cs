using System;
using Microsoft.EntityFrameworkCore;
using webapi.Application.Interfaces;
using webapi.Data;
using webapi.Models;

namespace webapi.Application.Services;

public class PacienteService : IPacienteService
{
    private readonly AppDbContext _db;

    public PacienteService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Paciente>> GetAll()
    {
        var pacientes = await _db.Pacientes.ToListAsync();

        if (pacientes == null || pacientes.Count == 0)
        {
            throw new Exception("No se encontraron pacientes.");
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
}
