using System;
using webapi.Modules.Doctores.dto;

namespace webapi.Modules.Doctores;

public interface IDoctorService
{
    Task<DoctorResponseDto> Create(DoctorCreateDto doctorCreateDto);
    Task<ICollection<DoctorResponseDto>> GetAll();
    Task<DoctorResponseDto> GetById(Guid id);
}
