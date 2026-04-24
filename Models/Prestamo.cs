using System.ComponentModel.DataAnnotations;

namespace libreria.Models;

public class Prestamo
{
    public int Id { get; set; }
    
    public DateTime FechaPrestamo { get; set; } = DateTime.Now;

    public DateTime? FechaDevolucion { get; set; }

    public bool Devuelto { get; set; } = false;
    
    
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    
    public int LibroId { get; set; }
    public Libro Libro { get; set; }
}