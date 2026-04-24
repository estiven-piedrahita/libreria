using Microsoft.AspNetCore.Mvc;
using libreria.Models;
using libreria.Services;

namespace libreria.Controllers;

public class LibroController : Controller
{
    private readonly LibroService _libroService;

    public LibroController(LibroService libroService)
    {
        _libroService = libroService;
    }

    public IActionResult Index()
    {
        var resultado = _libroService.ObtenerLibros();
        return View(resultado.Data);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Libro libro)
    {
        if (!ModelState.IsValid) return View(libro);

        _libroService.AgregarLibro(libro);
        TempData["Mensaje"] = "Libro registrado correctamente";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var resultado = _libroService.ObtenerId(id);
        if (!resultado.Success) return NotFound();
        return View(resultado.Data);
    }

    [HttpPost]
    public IActionResult Edit(Libro libro)
    {
        if (!ModelState.IsValid) return View(libro);

        _libroService.ActualizarLibro(libro);
        TempData["Mensaje"] = "Libro actualizado correctamente";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var resultado = _libroService.ObtenerId(id);
        if (!resultado.Success) return NotFound();
        return View(resultado.Data);
    }

    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        var resultado = _libroService.ObtenerId(id);
        if (resultado.Success)
            _libroService.EliminarLibro(resultado.Data);

        TempData["Mensaje"] = "Libro eliminado correctamente";
        return RedirectToAction("Index");
    }
}