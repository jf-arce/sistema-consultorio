using FluentValidation;
using webapi.Modules.Pacientes.Dto;

namespace webapi.Modules.Pacientes.Validators;

public class PacienteCreateValidator : AbstractValidator<PacienteCreateDto>
{
    public PacienteCreateValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre no puede tener más de 50 caracteres.");

        RuleFor(x => x.Apellido)
            .NotEmpty().WithMessage("El apellido es obligatorio.")
            .MaximumLength(50).WithMessage("El apellido no puede tener más de 50 caracteres.");

        RuleFor(x => x.Dni)
            .NotEmpty().WithMessage("El DNI es obligatorio.")
            .GreaterThan(0).WithMessage("El DNI debe ser un número positivo.");

        RuleFor(x => x.FechaNacimiento)
            .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
            .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("La fecha de nacimiento debe ser anterior a la fecha actual.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("El email no es válido.");

        RuleFor(x => x.Telefono)
            .Matches(@"^\d{10}$").WithMessage("El teléfono debe tener 10 dígitos.");

        RuleFor(x => x.Direccion)
            .MaximumLength(100).WithMessage("La dirección no puede tener más de 100 caracteres.");
    }
}
