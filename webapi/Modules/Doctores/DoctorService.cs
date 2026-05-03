using System;
using System.Net;
using Mapster;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Modules.Doctores.dto;
using webapi.Shared.Exceptions;

namespace webapi.Modules.Doctores;

public class DoctorService : IDoctorService
{
    private readonly AppDbContext _db;

    public DoctorService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<DoctorResponseDto> Create(DoctorCreateDto doctorCreateDto)
    {
        var doctor = doctorCreateDto.Adapt<Doctor>();
        
        _db.Doctores.Add(doctor);
        
        await _db.SaveChangesAsync();

        return doctor.Adapt<DoctorResponseDto>();
    }

    public async Task<ICollection<DoctorResponseDto>> GetAll()
    {
        var doctoresDto = await _db.Doctores.ProjectToType<DoctorResponseDto>().ToListAsync();

        if (doctoresDto == null || doctoresDto.Count == 0)
        {
            throw new CustomException(HttpStatusCode.NotFound, "No se encontraron doctores.");
        }

        return doctoresDto;
    }

    public Task<DoctorResponseDto> GetById(Guid id)
    {
        var doctorDto = _db.Doctores
            .ProjectToType<DoctorResponseDto>()
            .FirstOrDefault(d => d.Id == id);

        if (doctorDto == null)
        {
            throw new CustomException(HttpStatusCode.NotFound, $"Doctor con ID {id} no encontrado.");
        }

        return Task.FromResult(doctorDto);
    }
}
