using libreria.Models;
using libreria.Response;
using libreria.Data;

namespace libreria.Services;

public class LibroService
{
    private readonly AppDbContext _appDb;

    public LibroService(AppDbContext appDb)
    {
        _appDb = appDb;
    }

    public ServiceResponse<IEnumerable<Libro>> ObtenerLibros()
    {
        var libros = _appDb.Libros
            .OrderBy(l => l.Titulo)
            .ToList();
        return new ServiceResponse<IEnumerable<Libro>>
        {
            Success = true,
            Data = libros
        };
    }

    public ServiceResponse<Libro> AgregarLibro(Libro libro)
    {
        _appDb.Libros.Add(libro);
        var resultado = _appDb.SaveChanges();

        if (resultado > 0)
            return new ServiceResponse<Libro> { Data = libro, Success = true, Message = "Libro agregado correctamente" };

        return new ServiceResponse<Libro> { Data = libro, Success = false, Message = "No se pudo registrar el libro" };
    }

    public ServiceResponse<Libro> ObtenerId(int id)
    {
        var libro = _appDb.Libros.FirstOrDefault(l => l.Id == id);
        if (libro != null)
            return new ServiceResponse<Libro> { Success = true, Data = libro, Message = "Libro encontrado" };

        return new ServiceResponse<Libro> { Success = false, Data = null, Message = "Libro no encontrado" };
    }

    public ServiceResponse<Libro> ActualizarLibro(Libro libro)
    {
        var libroDb = _appDb.Libros.Find(libro.Id);
        if (libroDb == null)
            return new ServiceResponse<Libro> { Success = false, Message = "Libro no encontrado" };

        libroDb.Titulo = libro.Titulo;
        libroDb.Autor = libro.Autor;
        libroDb.Stock = libro.Stock;
        libroDb.FechaRegistro = libro.FechaRegistro;
        _appDb.SaveChanges();

        return new ServiceResponse<Libro> { Success = true, Data = libroDb, Message = "Libro actualizado correctamente" };
    }

    public ServiceResponse<Libro> EliminarLibro(Libro libro)
    {
        var libroDb = _appDb.Libros.Find(libro.Id);
        if (libroDb == null)
            return new ServiceResponse<Libro> { Success = false, Message = "Libro no encontrado" };

        _appDb.Libros.Remove(libroDb);
        _appDb.SaveChanges();

        return new ServiceResponse<Libro> { Success = true, Data = libro, Message = "Libro eliminado correctamente" };
    }
}
