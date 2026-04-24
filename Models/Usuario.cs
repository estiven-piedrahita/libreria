using System.ComponentModel.DataAnnotations;

namespace libreria.Models;

public class Usuario
{
    public int Id { get; set; }
    [Required]

    public string Nombre { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    
    public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}