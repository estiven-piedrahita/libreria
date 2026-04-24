using libreria.Models;
using libreria.Data;
using libreria.Response;


namespace libreria.Services;

public class UsuarioService
{
    private readonly AppDbContext _appDb ;

    public UsuarioService(AppDbContext appDb)
    {
        _appDb = appDb;
    }
    
    // metodo obtener read
    public ServiceResponse<IEnumerable<Usuario>> ObtenerUsuarios()
    {
        var users = _appDb.Usuarios.ToList();
        return new ServiceResponse<IEnumerable<Usuario>>()
        {
            Success = true,
            Data = users
        };
    }
    // metodo crear y guardar
    public ServiceResponse<Usuario> AgregarUsuario(Usuario usuario)
    {
        _appDb.Usuarios.Add(usuario);
        var resultado = _appDb.SaveChanges();

        if (resultado > 0)
        {
            return new ServiceResponse<Usuario>()
            {
                Data = usuario,
                Success = true,
                Message = "Usuario agregado correctamente"
            };
        }

        return new ServiceResponse<Usuario>()
        {
            Data = usuario,
            Success = false,
            Message = "Usuario no registrado correctamente"
        };
    }
    
    // tercer metodo editar
    public ServiceResponse<Usuario> ObtenerId(int id)
    {
        var usuario = _appDb.Usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario != null)
        {
            return new ServiceResponse<Usuario>()
            {
                Success = true,
                Data = usuario,
                Message = "Usuario encontrado"
            };
        }   

        return new ServiceResponse<Usuario>()
        {
            Success = false,
            Data = usuario,
            Message = "Usuario no encontrado "
        };
    }
    // cuarto meto actualizo y guardo
    public ServiceResponse<Usuario> actualizarUsuario(Usuario usuario)
    {
        var nuevoUsuario = _appDb.Usuarios.Find(usuario.Id);
        nuevoUsuario.Nombre = usuario.Nombre;
        nuevoUsuario.Email = usuario.Email;
        nuevoUsuario.FechaRegistro = usuario.FechaRegistro;
        _appDb.SaveChanges();

        return new ServiceResponse<Usuario>()
        {
            Success = true,
            Data = usuario,
            Message = "Usuario actualizado correctamente"
        };
    }
    
    // quinto metodo eliminar
    public ServiceResponse<Usuario> eliminarUsuario(Usuario usuario)
    {
        var usuarioDb = _appDb.Usuarios.Find(usuario.Id);

        _appDb.Usuarios.Remove(usuarioDb);
        _appDb.SaveChanges();

        return new ServiceResponse<Usuario>()
        {
            Success = true,
            Data = usuario,
            Message = "usuario eliminado correctamente"
        };
    }
    
}