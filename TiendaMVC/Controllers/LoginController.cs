using Microsoft.AspNetCore.Mvc;
using TiendaMVC.Interface;
using TiendaMVC.ViewModel;

namespace TiendaMVC.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    private readonly IAuthenticationService _authenticationService;

    public LoginController(ILogger<LoginController> logger, IAuthenticationService authenticationService)
    {
        _logger = logger;

        _authenticationService = authenticationService;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }
    // [HttpPost] Procesa el login
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
       //  Validación MVC: Chequea los atributos [Required] del ViewModel
        if (!ModelState.IsValid)
        {
            return View("Index", model);
        }
        // Intentamos loguear usando el servicio
        // CORRECCIÓN: Agregamos '?? string.Empty' para calmar al compilador
        if (_authenticationService.Login(model.Username ?? string.Empty, model.Password ?? string.Empty))
        {
            return RedirectToAction("Index", "Home");
        }
       // Error de Negocio: Si falla el login, agregamos el error al ModelState
        // El primer parámetro "" indica que es un error general del formulario
        ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
        
        return View("Index", model);
    }

    public IActionResult Logout()
    {
        _authenticationService.Logout();
        return RedirectToAction("Index");
    }
}