using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TiendaMVC.Models;
using TiendaMVC.Repository;
using TiendaMVC.ViewModel;
namespace TiendaMVC.Controllers;

public class PresupuestosController : Controller
{
    private readonly ILogger<ProductosController> _logger;
    private PresupuestosRepository _presupuestosRepository;
    private readonly ProductosRepository _productosRepository;
    public PresupuestosController(ILogger<ProductosController> logger)
    {
        _logger = logger;
        _presupuestosRepository = new PresupuestosRepository();
        _productosRepository = new ProductosRepository();
    }
    [HttpGet]
    public IActionResult Index()
    {
        var presupuestos = _presupuestosRepository.GetAll();
        var presupuestosViewModel = presupuestos.Select(p => new PresupuestoViewModel(p)).ToList();
        return View(presupuestosViewModel);

    }
    [HttpGet]
    public IActionResult Details(int id)
    {
        var presupuesto = _presupuestosRepository.DetallesPresupuestosID(id);
        if (presupuesto == null)
        {
            return NotFound();
        }
        return View(presupuesto);

    }
    [HttpGet]
    public IActionResult CrearPresupuesto()
    {
        var presupuestoViewModel = new CrearPresupuestoViewModel();
        return View(presupuestoViewModel);
    }


    [HttpPost]
    public IActionResult CrearPresupuesto(CrearPresupuestoViewModel presupuestoViewModel)
    {

        if (!ModelState.IsValid)
        {
            return View(presupuestoViewModel);
        }
        var presupuesto = new Presupuesto(presupuestoViewModel);
        _presupuestosRepository.Crear(presupuesto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditarPresupuesto(int id)
    {
        var presupuesto = _presupuestosRepository.DetallesPresupuestosID(id);
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
        if (!ModelState.IsValid)
        {
            return View(editarPresupuestoViewModel);
        }
        var presupuesto = new Presupuesto(editarPresupuestoViewModel);
        _presupuestosRepository.Modificar(presupuesto);
        return RedirectToAction("Index");

    }

    public IActionResult DeletePresupuesto(int id)
    {
        var Presupuesto = _presupuestosRepository.DetallesPresupuestosID(id);
        if (Presupuesto != null)
        {
            _presupuestosRepository.Eliminar(id);
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult AgregarProducto(int id)
    {
        if (_presupuestosRepository.DetallesPresupuestosID(id) == null)
        {
            return RedirectToAction("Index");
        }

        var productos = _productosRepository.GetAll();
        var ListaProductos = new SelectList(productos, "Id", "Descripcion");
        var agregarProductoViewModel = new AgregarProductoViewModel(id,ListaProductos);
        return View(agregarProductoViewModel);
    }

    [HttpPost]
    public IActionResult AgregarProducto(AgregarProductoViewModel agregarProductoViewModel)
    {
        if (!ModelState.IsValid)
        {
            var todosLosProductos = _productosRepository.GetAll();
            agregarProductoViewModel.ListaProductos = new SelectList(todosLosProductos, "Id", "Descripcion");
            return View(agregarProductoViewModel);
        }
        var producto = _productosRepository.DetallesProductosID(agregarProductoViewModel.IdProducto);
        if (producto==null)
        {
            var todosLosProductos = _productosRepository.GetAll();
            agregarProductoViewModel.ListaProductos = new SelectList(todosLosProductos, "Id", "Descripcion");
            return View(agregarProductoViewModel);
        }
        var detalle = new PresupuestosDetalle(producto,agregarProductoViewModel.Cantidad);
        _presupuestosRepository.AgregarProducto(agregarProductoViewModel.IdPresupuesto, detalle);
        return RedirectToAction("Details", new { id = agregarProductoViewModel.IdPresupuesto });
    }
}