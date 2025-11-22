using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TiendaMVC.Models;
using TiendaMVC.Interface;
using TiendaMVC.ViewModel;
namespace TiendaMVC.Controllers;

public class PresupuestosController : Controller
{
    private readonly ILogger<ProductosController> _logger;
    private readonly IPresupuestoRepository _presupuestoRepository;
    private readonly IProductoRepository _productoRepository;

    private readonly IAuthenticationService _authenticationService;


    public PresupuestosController(ILogger<ProductosController> logger,IPresupuestoRepository presupuestoRepository,IProductoRepository productoRepository,IAuthenticationService authenticationService)
    {
        _logger = logger;
        _presupuestoRepository = presupuestoRepository;
        _productoRepository = productoRepository;
        _authenticationService =authenticationService;

    }
    // --- MÉTODOS AUXILIARES DE SEGURIDAD ---

    // 1. Permiso estricto (Solo Admin): Para Modificaciones
    private IActionResult? CheckAdminPermissions()
    {
        if (!_authenticationService.IsAutenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        if (!_authenticationService.HasAccessLevel("Administrador"))
        {
            return RedirectToAction(nameof(AccesoDenegado));
        }
        return null; // Permiso concedido
    }
    // 2. Permiso amplio (Admin o Cliente): Para Lectura
    private IActionResult? CheckReadPermissions()
    {
        if (!_authenticationService.IsAutenticated())
        {
            return RedirectToAction("Index", "Login");
        }
        // Si NO es Admin Y TAMPOCO es Cliente -> Acceso Denegado
        if (!_authenticationService.HasAccessLevel("Administrador") && 
            !_authenticationService.HasAccessLevel("Cliente"))
        {
            return RedirectToAction(nameof(AccesoDenegado));
        }
        return null; // Permiso concedido
    }

    // --- ACCIONES DE LECTURA (Index, Details) ---

    [HttpGet]
    public IActionResult Index()
    {
        // Seguridad: Admin o Cliente
        var securityCheck = CheckReadPermissions();
        if (securityCheck != null) return securityCheck;

        var presupuestos = _presupuestoRepository.GetAll();
        var presupuestosViewModel = presupuestos.Select(p => new PresupuestoViewModel(p)).ToList();
        return View(presupuestosViewModel);

    }
    [HttpGet]
    public IActionResult Details(int id)
    {
        // Seguridad: Admin o Cliente
        var securityCheck = CheckReadPermissions();
        if (securityCheck != null) return securityCheck;

        var presupuesto = _presupuestoRepository.DetallesPresupuestosID(id);
        if (presupuesto == null)
        {
            return NotFound();
        }
        return View(presupuesto);

    }
    // --- ACCIONES DE ESCRITURA (Crear, Editar, Borrar, Agregar) ---

    [HttpGet]
    public IActionResult CrearPresupuesto()
    {
        // Seguridad: Solo Admin
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        var presupuestoViewModel = new CrearPresupuestoViewModel();
        return View(presupuestoViewModel);
    }


    [HttpPost]
    public IActionResult CrearPresupuesto(CrearPresupuestoViewModel presupuestoViewModel)
    {
        // Seguridad: Solo Admin
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        if (!ModelState.IsValid)
        {
            return View(presupuestoViewModel);
        }
        var presupuesto = new Presupuesto(presupuestoViewModel);
        _presupuestoRepository.Crear(presupuesto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditarPresupuesto(int id)
    {
        // Seguridad: Solo Admin
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        var presupuesto = _presupuestoRepository.DetallesPresupuestosID(id);
        if (presupuesto is null)
        {
            return RedirectToAction("Index");
        }
        else
        {
            var editarPresupuestoViewModel = new EditarPresupuestoViewModel(presupuesto);

            return View(editarPresupuestoViewModel);

        }
    }


    [HttpPost]
    public IActionResult EditarPresupuesto(EditarPresupuestoViewModel editarPresupuestoViewModel)
    {
        // Seguridad: Solo Admin
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        if (!ModelState.IsValid)
        {
            return View(editarPresupuestoViewModel);
        }
        var presupuesto = new Presupuesto(editarPresupuestoViewModel);
        _presupuestoRepository.Modificar(presupuesto);
        return RedirectToAction("Index");

    }

    public IActionResult DeletePresupuesto(int id)
    {
        // Seguridad: Solo Admin
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        var Presupuesto = _presupuestoRepository.DetallesPresupuestosID(id);
        if (Presupuesto != null)
        {
            _presupuestoRepository.Eliminar(id);
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult AgregarProducto(int id)
    {
        // Seguridad: Solo Admin
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        if (_presupuestoRepository.DetallesPresupuestosID(id) == null)
        {
            return RedirectToAction("Index");
        }

        var productos = _productoRepository.GetAll();
        var ListaProductos = new SelectList(productos, "Id", "Descripcion");
        var agregarProductoViewModel = new AgregarProductoViewModel(id,ListaProductos);
        return View(agregarProductoViewModel);
    }

    [HttpPost]
    public IActionResult AgregarProducto(AgregarProductoViewModel agregarProductoViewModel)
    {
        // Seguridad: Solo Admin
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;
        
        if (!ModelState.IsValid)
        {
            var todosLosProductos = _productoRepository.GetAll();
            agregarProductoViewModel.ListaProductos = new SelectList(todosLosProductos, "Id", "Descripcion");
            return View(agregarProductoViewModel);
        }
        var producto = _productoRepository.DetallesProductosID(agregarProductoViewModel.IdProducto);
        if (producto==null)
        {
            var todosLosProductos = _productoRepository.GetAll();
            agregarProductoViewModel.ListaProductos = new SelectList(todosLosProductos, "Id", "Descripcion");
            return View(agregarProductoViewModel);
        }
        var detalle = new PresupuestosDetalle(producto,agregarProductoViewModel.Cantidad);
        _presupuestoRepository.AgregarProducto(agregarProductoViewModel.IdPresupuesto, detalle);
        return RedirectToAction("Details", new { id = agregarProductoViewModel.IdPresupuesto });
    }
    [HttpGet]
    public IActionResult AccesoDenegado()
    {
        return View();
    }
}