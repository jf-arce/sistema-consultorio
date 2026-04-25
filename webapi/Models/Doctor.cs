namespace webapi.Models;

public class Doctor
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Especialidad { get; set; } = null!;

    public ICollection<Turno> Turnos { get; set; } = null!;
    public ICollection<Ficha> Fichas { get; set; } = null!;


    public string NombreCompleto()
    {
        return $"{Nombre} {Apellido}";
    }
}
