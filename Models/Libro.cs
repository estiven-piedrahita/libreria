namespace libreria.Models;

public class Libro
{
    public int Id { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public string Autor { get; set; } = string.Empty;

    public int Stock { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    // Relación
    public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}