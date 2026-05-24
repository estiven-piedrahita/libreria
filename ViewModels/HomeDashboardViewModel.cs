namespace libreria.ViewModels;

public class HomeDashboardViewModel
{
    public int TotalUsuarios { get; set; }
    public int TotalLibros { get; set; }
    public int PrestamosActivos { get; set; }
    public int LibrosSinStock { get; set; }
    public int PrestamosDevueltos { get; set; }
}
