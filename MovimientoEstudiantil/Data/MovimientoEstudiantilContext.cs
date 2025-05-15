using Microsoft.EntityFrameworkCore;
using MovimientoEstudiantil.Models;

namespace MovimientoEstudiantil.Data
{
    public class MovimientoEstudiantilContext : DbContext
    {
        public MovimientoEstudiantilContext(DbContextOptions<MovimientoEstudiantilContext> options)
            : base(options)
        {
        }

        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Sede> Sedes { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Provincia → Sede (1:N)
            modelBuilder.Entity<Sede>()
                .HasOne(s => s.Provincia)
                .WithMany(p => p.Sedes)
                .HasForeignKey(s => s.idProvincia)
                .OnDelete(DeleteBehavior.Restrict);

            // Provincia → Estudiante (1:N)
            modelBuilder.Entity<Estudiante>()
                .HasOne(e => e.Provincia)
                .WithMany() // no existe ICollection<Estudiante> en Provincia
                .HasForeignKey(e => e.provinciaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sede → Estudiante (1:N)
            modelBuilder.Entity<Estudiante>()
                .HasOne(e => e.Sede)
                .WithMany(s => s.Estudiantes)
                .HasForeignKey(e => e.sedeId)
                .OnDelete(DeleteBehavior.Restrict);

            // (Opcional) seed data para desarrollo
            modelBuilder.Entity<Provincia>().HasData(
                new Provincia { idProvincia = 1, nombre = "San José" }
            );
            modelBuilder.Entity<Sede>().HasData(
                new Sede { idSede = 1, nombre = "Ciudad Universitaria Rodrigo Facio", idProvincia = 1 }
            );
            modelBuilder.Entity<Estudiante>().HasData(
                new Estudiante
                {
                    idEstudiante = 1,
                    correo = "gustavo@ucr.ac.cr",
                    provinciaId = 1,
                    sedeId = 1,
                    satisfaccionCarrera = "Si",
                    anioIngreso = 2023
                }
            );
        }
    }
}
