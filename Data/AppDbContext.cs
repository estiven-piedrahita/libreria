using Microsoft.EntityFrameworkCore;
using libreria.Models;

namespace libreria.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Libro> Libros { get; set; }
    public DbSet<Prestamo> Prestamos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<Prestamo>()
            .HasOne(p => p.Usuario)
            .WithMany(u => u.Prestamos)
            .HasForeignKey(p => p.UsuarioId);

        
        modelBuilder.Entity<Prestamo>()
            .HasOne(p => p.Libro)
            .WithMany(l => l.Prestamos)
            .HasForeignKey(p => p.LibroId);
    }
}