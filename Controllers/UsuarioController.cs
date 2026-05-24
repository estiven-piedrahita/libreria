using Microsoft.AspNetCore.Mvc;
using libreria.Models;
using libreria.Services;

namespace libreria.Controllers;

public class UsuarioController : Controller
{
    private readonly UsuarioService _usuarioService;

    public UsuarioController(UsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public IActionResult Index()
    {
        var resultado = _usuarioService.ObtenerUsuarios();
        return View(resultado.Data);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Usuario usuario)
    {
        if (!ModelState.IsValid) return View(usuario);

        _usuarioService.AgregarUsuario(usuario);
        TempData["Mensaje"] = "Usuario registrado correctamente";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var resultado = _usuarioService.ObtenerId(id);
        if (!resultado.Success) return NotFound();
        return View(resultado.Data);
    }

    [HttpPost]
    public IActionResult Edit(Usuario usuario)
    {
        if (!ModelState.IsValid) return View(usuario);

        _usuarioService.actualizarUsuario(usuario);
        TempData["Mensaje"] = "Usuario actualizado correctamente";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var resultado = _usuarioService.ObtenerId(id);
        if (!resultado.Success) return NotFound();
        return View(resultado.Data!);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var resultado = _usuarioService.ObtenerId(id);
        if (resultado.Success)
            _usuarioService.eliminarUsuario(resultado.Data!);

        TempData["Mensaje"] = "Usuario eliminado correctamente";
        return RedirectToAction("Index");
    }
}
