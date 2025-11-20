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

    public ProductosController(ILogger<ProductosController> logger,IProductoRepository productoRepository,IAuthenticationService authenticationService)
    {
        _logger = logger;
        _productoRepository = productoRepository;
        _authenticationService = authenticationService;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var productos = _productoRepository.GetAll();
        var productosViewModel = productos.Select(p => new ProductoViewModel(p)).ToList();
        return View(productosViewModel);
    }
    [HttpGet]
    public IActionResult CrearProducto()
    {
        var productoViewModel = new CrearProductoViewModel();
        return View(productoViewModel);
    }


    [HttpPost]
    public IActionResult CrearProducto(CrearProductoViewModel productoViewModel)
    {
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
        if (!ModelState.IsValid)
        {
            return View(editarProductoViewModel);
        }
        var producto = new Producto(editarProductoViewModel);
        _productoRepository.Modificar(producto);
        return RedirectToAction("Index");

    }

    public IActionResult DeleteProducto(int idProducto)
    {
        var producto = _productoRepository.DetallesProductosID(idProducto);
        if (producto != null)
        {
            _productoRepository.Eliminar(idProducto);
        }
        return RedirectToAction("Index");
    }
}