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
        return View(resultado.Data);
    }

    public IActionResult Create()
    {
        CargarDropdowns();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Prestamo prestamo)
    {
        

        _prestamoService.AgregarPrestamo(prestamo);
        TempData["Mensaje"] = "Préstamo registrado correctamente";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var resultado = _prestamoService.ObtenerId(id);
        if (!resultado.Success) return NotFound();
        CargarDropdowns();
        return View(resultado.Data);
    }

    [HttpPost]
    public IActionResult Edit(Prestamo prestamo)
    {
       

        _prestamoService.ActualizarPrestamo(prestamo);
        TempData["Mensaje"] = "Préstamo actualizado correctamente";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var resultado = _prestamoService.ObtenerId(id);
        if (!resultado.Success) return NotFound();
        return View(resultado.Data);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        var resultado = _prestamoService.ObtenerId(id);
        if (resultado.Success)
            _prestamoService.EliminarPrestamo(resultado.Data);

        TempData["Mensaje"] = "Préstamo eliminado correctamente";
        return RedirectToAction("Index");
    }

    private void CargarDropdowns()
    {
        ViewBag.Usuarios = new SelectList(_usuarioService.ObtenerUsuarios().Data, "Id", "Nombre");
        ViewBag.Libros = new SelectList(_libroService.ObtenerLibros().Data, "Id", "Titulo");
    }
}
