using FluentValidation;
using webapi.Modules.Pacientes.Dto;

namespace webapi.Modules.Pacientes.Validators;

public class PacienteUpdateValidator : AbstractValidator<PacienteUpdateDto>
{
    public PacienteUpdateValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().MaximumLength(50).When(x => x.Nombre != null);

        RuleFor(x => x.Apellido)
            .NotEmpty().MaximumLength(50).When(x => x.Apellido != null);

        RuleFor(x => x.Dni)
            .GreaterThan(0).When(x => x.Dni.HasValue);

        RuleFor(x => x.FechaNacimiento)
            .LessThan(DateOnly.FromDateTime(DateTime.Now)).When(x => x.FechaNacimiento.HasValue);

        RuleFor(x => x.Email)
            .EmailAddress().When(x => x.Email != null);

        RuleFor(x => x.Telefono)
            .Matches(@"^\d{10}$").When(x => x.Telefono != null);

        RuleFor(x => x.Direccion)
            .MaximumLength(100).When(x => x.Direccion != null);
    }
}
