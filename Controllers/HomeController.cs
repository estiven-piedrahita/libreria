using System.Diagnostics;
using libreria.Data;
using Microsoft.AspNetCore.Mvc;
using libreria.Models;
using libreria.ViewModels;

namespace libreria.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var dashboard = new HomeDashboardViewModel
        {
            TotalUsuarios = _context.Usuarios.Count(),
            TotalLibros = _context.Libros.Count(),
            PrestamosActivos = _context.Prestamos.Count(p => !p.Devuelto),
            LibrosSinStock = _context.Libros.Count(l => l.Stock <= 0),
            PrestamosDevueltos = _context.Prestamos.Count(p => p.Devuelto)
        };

        return View(dashboard);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
