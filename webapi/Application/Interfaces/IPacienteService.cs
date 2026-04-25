using System;
using webapi.Models;

namespace webapi.Application.Interfaces;

public interface IPacienteService
{
    Task<IEnumerable<Paciente>> GetAll();
}
