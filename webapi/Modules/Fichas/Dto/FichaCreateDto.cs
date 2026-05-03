namespace webapi.Modules.Fichas.Dto
{
    public class FichaCreateDto
    {
        public Guid DoctorId { get; set; }
        public Guid PacienteId { get; set; }
    }
}
