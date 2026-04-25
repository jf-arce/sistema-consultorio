using System;
using webapi.Application.DTOs;
using webapi.Models;

namespace webapi.Application.Interfaces;

public interface IPacienteService
{
    Task<Paciente> Create(CreatePacienteDto createPacienteDto);
    Task<IEnumerable<Paciente>> GetAll();
    Task<Paciente> GetById(int id);
}
