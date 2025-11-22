using Microsoft.AspNetCore.Mvc;
using TiendaMVC.Models;
using TiendaMVC.Interface;
using TiendaMVC.ViewModel;


namespace TiendaMVC.Controllers;

public class ProductosController : Controller
{
    private readonly ILogger<ProductosController> _logger;
    private readonly IProductoRepository _productoRepository;
    private readonly IAuthenticationService _authenticationService;

    public ProductosController(ILogger<ProductosController> logger, IProductoRepository productoRepository, IAuthenticationService authenticationService)
    {
        _logger = logger;
        _productoRepository = productoRepository;
        _authenticationService = authenticationService;
    }
    // Método helper para centralizar la lógica de seguridad
    private IActionResult ? CheckAdminPermissions()
    {
        // 1. No logueado? -> vuelve al login
        if (!_authenticationService.IsAutenticated())
        {
            return RedirectToAction("Index", "Login");
        }

        // 2. No es Administrador? -> Da Error
        if (!_authenticationService.HasAccessLevel("Administrador"))
        {
            // Llamamos a AccesoDenegado (llama a la vista correspondiente de Productos)
            return RedirectToAction(nameof(AccesoDenegado));
        }
        return null; // Permiso concedido
    }
    public IActionResult AccesoDenegado()
    {
        // El usuario está logueado, pero no tiene el rol suficiente.
        return View();
    }

    [HttpGet]
    public IActionResult Index()
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        var productos = _productoRepository.GetAll();
        var productosViewModel = productos.Select(p => new ProductoViewModel(p)).ToList();
        return View(productosViewModel);
    }
    [HttpGet]
    public IActionResult CrearProducto()
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        var productoViewModel = new CrearProductoViewModel();
        return View(productoViewModel);
    }


    [HttpPost]
    public IActionResult CrearProducto(CrearProductoViewModel productoViewModel)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        if (!ModelState.IsValid)
        {
            return View(productoViewModel);
        }
        var producto = new Producto(productoViewModel);
        _productoRepository.Alta(producto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditarProducto(int id)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        var producto = _productoRepository.DetallesProductosID(id);
        if (producto is null)
        {
            return RedirectToAction("Index");
        }
        else
        {
            var editarProductoViewModel = new EditarProductoViewModel(producto);

            return View(editarProductoViewModel);

        }

    }


    [HttpPost]
    public IActionResult EditarProducto(EditarProductoViewModel editarProductoViewModel)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        if (!ModelState.IsValid)
        {
            return View(editarProductoViewModel);
        }
        var producto = new Producto(editarProductoViewModel);
        _productoRepository.Modificar(producto);
        return RedirectToAction("Index");

    }

    public IActionResult DeleteProducto(int id)
    {
        var securityCheck = CheckAdminPermissions();
        if (securityCheck != null) return securityCheck;

        var producto = _productoRepository.DetallesProductosID(id);
        if (producto != null)
        {
            _productoRepository.Eliminar(id);
        }
        return RedirectToAction("Index");
    }
}