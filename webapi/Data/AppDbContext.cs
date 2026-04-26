using Microsoft.EntityFrameworkCore;
using webapi.Modules.Consultas;
using webapi.Modules.Doctores;
using webapi.Modules.Fichas;
using webapi.Modules.Pacientes;
using webapi.Modules.Turnos;
using webapi.Shared;

namespace webapi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public DbSet<Paciente> Pacientes { get; set; } = null!;
    public DbSet<Ficha> Fichas { get; set; } = null!;
    public DbSet<Consulta> Consultas { get; set; } = null!;
    public DbSet<Doctor> Doctores { get; set; } = null!;
    public DbSet<Turno> Turnos { get; set; } = null!;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                entry.Entity.UpdatedAt = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ISoftDelete>())
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.DeletedAt = now;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var paciente = modelBuilder.Entity<Paciente>();
        paciente.HasIndex(p => p.Dni).IsUnique();
        paciente.HasOne(p => p.Ficha)
            .WithOne(f => f.Paciente)
            .HasForeignKey<Ficha>(f => f.PacienteId);
        paciente.HasMany(p => p.Turnos)
            .WithOne(t => t.Paciente)
            .HasForeignKey(t => t.PacienteId);
        paciente.HasQueryFilter(p => p.DeletedAt == null);

        var ficha = modelBuilder.Entity<Ficha>();
        ficha.HasMany(f => f.Consultas)
            .WithOne(c => c.Ficha)
            .HasForeignKey(c => c.FichaId);
        ficha.HasQueryFilter(f => f.DeletedAt == null);

        var doctor = modelBuilder.Entity<Doctor>();
        doctor.HasMany(d => d.Turnos)
            .WithOne(t => t.Doctor)
            .HasForeignKey(t => t.DoctorId);
        doctor.HasMany(d => d.Fichas)
            .WithOne(f => f.Doctor)
            .HasForeignKey(f => f.DoctorId);
        doctor.HasQueryFilter(d => d.DeletedAt == null);

        var turno = modelBuilder.Entity<Turno>();
        turno.Property(t => t.Estado).HasConversion<string>();
        turno.HasQueryFilter(t => t.DeletedAt == null);

        modelBuilder.Entity<Consulta>()
            .HasQueryFilter(c => c.DeletedAt == null);
    }
}
