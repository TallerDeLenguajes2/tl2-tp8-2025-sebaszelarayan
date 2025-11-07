using Microsoft.AspNetCore.Mvc;
using Tienda.Models;
using Tienda.Repository;

public class PresupuestosController : Controller
{
    private PresupuestosRepository _presupuestosRepository;
    public PresupuestosController()
    {
        _presupuestosRepository = new PresupuestosRepository();
    }
    [HttpGet]
    public IActionResult Index()
    {
        List<Presupuesto> presupuestos = _presupuestosRepository.ListarPresupuestos();
        return View(presupuestos);

    }
    [HttpGet]
    public IActionResult Details(int idPresupuesto)
    {
        var presupuesto = _presupuestosRepository.DetallesPresupuestosID(idPresupuesto);
        if (presupuesto == null)
        {
            // Si no se encuentra, devolvemos una página de error 404.
            return NotFound();
        }
        return View(presupuesto);

    }
    [HttpGet]
    public IActionResult CrearPresupuesto()
    {
        var Presupuesto = new Presupuesto();
        return View(Presupuesto);
    }


    [HttpPost]
    public IActionResult CrearPresupuesto(Presupuesto Presupuesto)
    {

        _presupuestosRepository.Crear(Presupuesto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditarPresupuesto(int idPresupuesto)
    {
        var Presupuesto = _presupuestosRepository.DetallesPresupuestosID(idPresupuesto);
        if (Presupuesto is null) RedirectToAction("Index");
        return View(Presupuesto);
    }


    [HttpPost]
    public IActionResult EditarPresupuesto(Presupuesto Presupuesto)
    {
        _presupuestosRepository.Modificar(Presupuesto);
        return RedirectToAction("Index");

    }

    public IActionResult DeletePresupuesto(int idPresupuesto)
    {
        var Presupuesto = _presupuestosRepository.DetallesPresupuestosID(idPresupuesto);
        if (Presupuesto != null)
        {
            _presupuestosRepository.Eliminar(idPresupuesto);
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult AgregarProducto(int idPresupuesto)
    {
        if (_presupuestosRepository.DetallesPresupuestosID(idPresupuesto) == null)
        {
            return RedirectToAction("Index");
        }
        // Pasamos el idPresupuesto a la vista usando ViewBag
        //    para que el <form> pueda construir su URL de envío.
        ViewBag.IdPresupuesto = idPresupuesto;
        var detalle = new PresupuestosDetalle();
        return View(detalle);
    }

    
    [HttpPost]
    public IActionResult AgregarProducto(int idPresupuesto, PresupuestosDetalle detalle)
    {
        _presupuestosRepository.AgregarProducto(idPresupuesto, detalle);
        return RedirectToAction("Details", new { idPresupuesto });
    }
}