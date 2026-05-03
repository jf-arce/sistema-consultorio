using System;
using FluentValidation;
using webapi.Modules.Doctores.dto;

namespace webapi.Modules.Doctores.validators;

public class DoctorCreateValidator : AbstractValidator<DoctorCreateDto>
{
    public DoctorCreateValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.");

        RuleFor(x => x.Apellido)
            .NotEmpty().WithMessage("El apellido es obligatorio.")
            .MaximumLength(50).WithMessage("El apellido no puede exceder los 50 caracteres.");

        RuleFor(x => x.Especialidad)
            .NotEmpty().WithMessage("La especialidad es obligatoria.")
            .MaximumLength(100).WithMessage("La especialidad no puede exceder los 100 caracteres.");
    }
}
