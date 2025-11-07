using Microsoft.AspNetCore.Mvc;
using Tienda.Models;
using Tienda.Repository;

public class ProductosController:Controller
{
    private ProductosRepository _productosRepository;
    public ProductosController()
    {
        _productosRepository = new ProductosRepository();
    }
    [HttpGet]
    public IActionResult Index()
    {
        List<Producto> productos = _productosRepository.GetAll();
        return View(productos);
    }
    [HttpGet]
    public IActionResult CrearProducto()
    {
        var producto = new Producto();
        return View(producto);
    }

    
    [HttpPost]
    public IActionResult CrearProducto(Producto producto)
    {

        _productosRepository.Alta(producto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditarProducto(int idProducto)
    {
        var producto = _productosRepository.DetallesProductosID(idProducto);
        if (producto is null) RedirectToAction("Index");
        return View(producto);
    }


    [HttpPost]
    public IActionResult EditarProducto(Producto producto)
    {
        _productosRepository.Modificar(producto);
        return RedirectToAction("Index");

    }
    
    public IActionResult DeleteProducto(int idProducto)
    {
        var producto = _productosRepository.DetallesProductosID(idProducto);
        if (producto!=null)
        {
        _productosRepository.Eliminar(idProducto);
        }
        return RedirectToAction("Index");
    }
}