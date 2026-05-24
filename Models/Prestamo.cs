using System.ComponentModel.DataAnnotations;

namespace libreria.Models;

public class Prestamo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La fecha de préstamo es obligatoria")]
    public DateTime FechaPrestamo { get; set; } = DateTime.Now;

    public DateTime? FechaDevolucion { get; set; }

    public bool Devuelto { get; set; } = false;
    
    [Range(1, int.MaxValue, ErrorMessage = "Selecciona un usuario")]
    public int UsuarioId { get; set; }

    public Usuario? Usuario { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecciona un libro")]
    public int LibroId { get; set; }
    public Libro? Libro { get; set; }
}
