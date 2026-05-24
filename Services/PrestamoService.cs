    using libreria.Data;
    using libreria.Models;
    using libreria.Response;
    using Microsoft.EntityFrameworkCore;

    namespace libreria.Services;

    public class PrestamoService
    {
        private readonly AppDbContext _appDb;

        public PrestamoService(AppDbContext appDb)
        {
            _appDb = appDb;
        }

        public ServiceResponse<IEnumerable<Prestamo>> ObtenerPrestamos()
        {
            var prestamos = _appDb.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .OrderByDescending(p => p.FechaPrestamo)
                .ToList();
            return new ServiceResponse<IEnumerable<Prestamo>> { Success = true, Data = prestamos };
        }

        public ServiceResponse<Prestamo> AgregarPrestamo(Prestamo prestamo)
        {
            var libro = _appDb.Libros.Find(prestamo.LibroId);
            if (libro == null)
                return new ServiceResponse<Prestamo> { Data = prestamo, Success = false, Message = "El libro seleccionado no existe" };

            if (!prestamo.Devuelto)
            {
                if (libro.Stock <= 0)
                    return new ServiceResponse<Prestamo> { Data = prestamo, Success = false, Message = "No hay stock disponible para este libro" };

                libro.Stock--;
            }

            if (prestamo.Devuelto && prestamo.FechaDevolucion == null)
                prestamo.FechaDevolucion = DateTime.Now;

            _appDb.Prestamos.Add(prestamo);
            var resultado = _appDb.SaveChanges();

            if (resultado > 0)
                return new ServiceResponse<Prestamo> { Data = prestamo, Success = true, Message = "Préstamo registrado correctamente" };

            return new ServiceResponse<Prestamo> { Data = prestamo, Success = false, Message = "No se pudo registrar el préstamo" };
        }

        public ServiceResponse<Prestamo> ObtenerId(int id)
        {
            var prestamo = _appDb.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .FirstOrDefault(p => p.Id == id);

            if (prestamo != null)
                return new ServiceResponse<Prestamo> { Success = true, Data = prestamo, Message = "Préstamo encontrado" };

            return new ServiceResponse<Prestamo> { Success = false, Data = null, Message = "Préstamo no encontrado" };
        }

        public ServiceResponse<Prestamo> ActualizarPrestamo(Prestamo prestamo)
        {
            var prestamoDb = _appDb.Prestamos
                .Include(p => p.Libro)
                .FirstOrDefault(p => p.Id == prestamo.Id);
            if (prestamoDb == null)
                return new ServiceResponse<Prestamo> { Success = false, Message = "Préstamo no encontrado" };

            var libroNuevo = _appDb.Libros.Find(prestamo.LibroId);
            if (libroNuevo == null)
                return new ServiceResponse<Prestamo> { Success = false, Message = "El libro seleccionado no existe" };

            var estabaActivo = !prestamoDb.Devuelto;
            var quedaraActivo = !prestamo.Devuelto;

            if (estabaActivo)
            {
                var libroAnterior = _appDb.Libros.Find(prestamoDb.LibroId);
                if (libroAnterior != null)
                    libroAnterior.Stock++;
            }

            if (quedaraActivo)
            {
                if (libroNuevo.Stock <= 0)
                    return new ServiceResponse<Prestamo> { Success = false, Message = "No hay stock disponible para este libro" };

                libroNuevo.Stock--;
            }

            prestamoDb.FechaPrestamo = prestamo.FechaPrestamo;
            prestamoDb.Devuelto = prestamo.Devuelto;
            prestamoDb.FechaDevolucion = prestamo.Devuelto && prestamo.FechaDevolucion == null
                ? DateTime.Now
                : prestamo.FechaDevolucion;
            prestamoDb.UsuarioId = prestamo.UsuarioId;
            prestamoDb.LibroId = prestamo.LibroId;
            _appDb.SaveChanges();

            return new ServiceResponse<Prestamo> { Success = true, Data = prestamoDb, Message = "Préstamo actualizado correctamente" };
        }

        public ServiceResponse<Prestamo> EliminarPrestamo(Prestamo prestamo)
        {
            var prestamoDb = _appDb.Prestamos.Find(prestamo.Id);
            if (prestamoDb == null)
                return new ServiceResponse<Prestamo> { Success = false, Message = "Préstamo no encontrado" };

            if (!prestamoDb.Devuelto)
            {
                var libro = _appDb.Libros.Find(prestamoDb.LibroId);
                if (libro != null)
                    libro.Stock++;
            }

            _appDb.Prestamos.Remove(prestamoDb);
            _appDb.SaveChanges();

            return new ServiceResponse<Prestamo> { Success = true, Data = prestamoDb, Message = "Préstamo eliminado correctamente" };
        }
    }
