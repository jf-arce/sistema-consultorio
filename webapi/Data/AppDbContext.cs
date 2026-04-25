using System;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

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

        var ficha = modelBuilder.Entity<Ficha>();
        ficha.HasMany(f => f.Consultas)
            .WithOne(c => c.Ficha)
            .HasForeignKey(c => c.FichaId);

        var doctor = modelBuilder.Entity<Doctor>();
        doctor.HasMany(d => d.Turnos)
            .WithOne(t => t.Doctor)
            .HasForeignKey(t => t.DoctorId);
        doctor.HasMany(d => d.Fichas)
            .WithOne(f => f.Doctor)
            .HasForeignKey(f => f.DoctorId);


        var turno = modelBuilder.Entity<Turno>();
        turno.Property(t => t.Estado)
            .HasConversion<string>();
    }
}
