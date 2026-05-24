using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using libreria.Models;
using libreria.Services;

namespace libreria.Controllers;

public class PrestamoController : Controller
{
    private readonly PrestamoService _prestamoService;
    private readonly UsuarioService _usuarioService;
    private readonly LibroService _libroService;

    public PrestamoController(PrestamoService prestamoService, UsuarioService usuarioService, LibroService libroService)
    {
        _prestamoService = prestamoService;
        _usuarioService = usuarioService;
        _libroService = libroService;
    }

    public IActionResult Index()
    {
        var resultado = _prestamoService.ObtenerPrestamos();
        return View(resultado.Data ?? Enumerable.Empty<Prestamo>());
    }

    public IActionResult Create()
    {
        CargarDropdowns();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Prestamo prestamo)
    {
        ModelState.Remove(nameof(Prestamo.Usuario));
        ModelState.Remove(nameof(Prestamo.Libro));

        if (!ModelState.IsValid)
        {
            CargarDropdowns(prestamo.LibroId);
            return View(prestamo);
        }

        var resultado = _prestamoService.AgregarPrestamo(prestamo);
        if (!resultado.Success)
        {
            ModelState.AddModelError(string.Empty, resultado.Message);
            CargarDropdowns(prestamo.LibroId);
            return View(prestamo);
        }

        TempData["Mensaje"] = resultado.Message;
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var resultado = _prestamoService.ObtenerId(id);
        if (!resultado.Success) return NotFound();
        CargarDropdowns(resultado.Data!.LibroId);
        return View(resultado.Data);
    }

    [HttpPost]
    public IActionResult Edit(Prestamo prestamo)
    {
        ModelState.Remove(nameof(Prestamo.Usuario));
        ModelState.Remove(nameof(Prestamo.Libro));

        if (!ModelState.IsValid)
        {
            CargarDropdowns(prestamo.LibroId);
            return View(prestamo);
        }

        var resultado = _prestamoService.ActualizarPrestamo(prestamo);
        if (!resultado.Success)
        {
            ModelState.AddModelError(string.Empty, resultado.Message);
            CargarDropdowns(prestamo.LibroId);
            return View(prestamo);
        }

        TempData["Mensaje"] = resultado.Message;
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var resultado = _prestamoService.ObtenerId(id);
        if (!resultado.Success) return NotFound();
        return View(resultado.Data!);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var resultado = _prestamoService.ObtenerId(id);
        if (resultado.Success)
            _prestamoService.EliminarPrestamo(resultado.Data!);

        TempData["Mensaje"] = "Préstamo eliminado correctamente";
        return RedirectToAction("Index");
    }

    private void CargarDropdowns(int? libroSeleccionado = null)
    {
        var usuarios = _usuarioService.ObtenerUsuarios().Data ?? Enumerable.Empty<Usuario>();
        var libros = (_libroService.ObtenerLibros().Data ?? Enumerable.Empty<Libro>())
            .Where(l => l.Stock > 0 || l.Id == libroSeleccionado)
            .Select(l => new
            {
                l.Id,
                Titulo = $"{l.Titulo} - {l.Autor} ({l.Stock} disponibles)"
            });

        ViewBag.Usuarios = new SelectList(usuarios, "Id", "Nombre");
        ViewBag.Libros = new SelectList(libros, "Id", "Titulo");
    }
}
