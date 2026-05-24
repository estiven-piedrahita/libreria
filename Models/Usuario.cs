using System.ComponentModel.DataAnnotations;

namespace libreria.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Ingresa un correo válido")]
    [StringLength(120, ErrorMessage = "El correo no puede superar 120 caracteres")]
    public string Email { get; set; } = string.Empty;

    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    
    public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
