using System.ComponentModel.DataAnnotations;

namespace libreria.Models;

public class Libro
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es obligatorio")]
    [StringLength(120, ErrorMessage = "El título no puede superar 120 caracteres")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "El autor es obligatorio")]
    [StringLength(100, ErrorMessage = "El autor no puede superar 100 caracteres")]
    public string Autor { get; set; } = string.Empty;

    [Range(0, 9999, ErrorMessage = "El stock no puede ser negativo")]
    public int Stock { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    // Relación
    public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
