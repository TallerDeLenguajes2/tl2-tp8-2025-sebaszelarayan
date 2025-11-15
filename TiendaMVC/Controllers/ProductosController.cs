using Microsoft.AspNetCore.Mvc;
using TiendaMVC.Models;
using TiendaMVC.Repository;
using TiendaMVC.ViewModel;

namespace TiendaMVC.Controllers;

public class ProductosController : Controller
{
    private readonly ILogger<ProductosController> _logger;
    private readonly ProductosRepository _productosRepository;
    
    public ProductosController(ILogger<ProductosController> logger)
    {
        _logger = logger;
        _productosRepository = new ProductosRepository();
    }
    [HttpGet]
    public IActionResult Index()
    {
        var productos = _productosRepository.GetAll();
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
        _productosRepository.Alta(producto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditarProducto(int id)
    {
        var producto = _productosRepository.DetallesProductosID(id);
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
        _productosRepository.Modificar(producto);
        return RedirectToAction("Index");

    }

    public IActionResult DeleteProducto(int idProducto)
    {
        var producto = _productosRepository.DetallesProductosID(idProducto);
        if (producto != null)
        {
            _productosRepository.Eliminar(idProducto);
        }
        return RedirectToAction("Index");
    }
}