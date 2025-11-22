using TiendaMVC.Interface;

namespace TiendaMVC.Service;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public AuthenticationService(IUsuarioRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _usuarioRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public bool Login(string username, string password)
    {
        //  Intentamos buscar el usuario en la base de datos
        var usuario = _usuarioRepository.GetUser(username, password);

        //  VALIDACIÓN DE SEGURIDAD:
        // Si 'usuario' es null, significa que el usuario no existe o la contraseña está mal.
        // Debemos retornar false inmediatamente.
        if (usuario == null)
        {
            return false;
        }
        var context = _httpContextAccessor.HttpContext;
        // Validación técnica: Aseguramos que podemos escribir en la sesión
        if (context != null)
        {
            // 4. Guardado Seguro (Null Safety):
            // Usamos '??' para asegurar que nunca pasamos un null a SetString.

            context.Session.SetString("IsAuthenticated", "true");
            context.Session.SetString("User", usuario.Username ?? string.Empty);
            context.Session.SetString("Nombre", usuario.Nombre ?? string.Empty);

            // Si el rol es null en la BD, le asignamos "Cliente" por defecto
            context.Session.SetString("Rol", usuario.Rol ?? "Cliente");

            return true; // Login exitoso
        }

        return false; // Falló por contexto nulo
    }

    public void Logout()
    {
        var context = _httpContextAccessor.HttpContext;
        // Es mejor no lanzar excepción en logout, solo verificar si existe
        if (context != null)
        {
            context.Session.Clear();
        }

    }
    public bool IsAutenticated()
    {
        var context = _httpContextAccessor.HttpContext;
        // Si no hay contexto, técnicamente no está autenticado, retornamos false en vez de error.
        if (context == null)
        {
            return false;
        }
        return context.Session.GetString("IsAuthenticated") == "true";

    }

    public bool HasAccessLevel(string requiredAccessLevel)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null) return false;

        var userRole = context.Session.GetString("Rol");

        // Verificamos que el rol no sea nulo y coincida
        return userRole != null && userRole == requiredAccessLevel;
    }
}