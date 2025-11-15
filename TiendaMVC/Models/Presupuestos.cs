namespace TiendaMVC.Models;

using TiendaMVC.ViewModel;
public class Presupuesto
{
    const decimal IVA = 0.21m;
    private int id;
    private string? nombreDestinatario;
    private DateTime fechaCreacion;
    private List<PresupuestosDetalle>? detalle;

    public int Id { get => id; set => id = value; }
    public string? NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestosDetalle>? Detalle { get => detalle; set => detalle = value; }

    public Presupuesto(string? nombreDestinatario, DateTime fechaCreacion, List<PresupuestosDetalle>? detalle)
    {
        this.nombreDestinatario = nombreDestinatario;
        this.fechaCreacion = fechaCreacion;
        this.detalle = detalle;
    }
    public Presupuesto()
    {
        Detalle = new List<PresupuestosDetalle>();
    }
    public Presupuesto(CrearPresupuestoViewModel presupuestoViewModel)
    {
        nombreDestinatario = presupuestoViewModel.NombreDestinatario;
        fechaCreacion = presupuestoViewModel.FechaCreacion;
        detalle = presupuestoViewModel.Detalle;
    }


    public Presupuesto(EditarPresupuestoViewModel presupuestoViewModel)
    {
        id = presupuestoViewModel.Id;
        nombreDestinatario = presupuestoViewModel.NombreDestinatario;
        fechaCreacion = presupuestoViewModel.FechaCreacion;
        detalle = presupuestoViewModel.Detalle;
    }

    public decimal montoPresupuesto()
    {
        decimal monto = 0;
        if (Detalle != null)
        {
            foreach (var item in Detalle)
            {
                if (item.Producto != null)
                {
                    monto += item.Producto.Precio;
                }
            }
        }
        return monto;
    }



    public decimal MontoPresupuestoConIva()
    {
        return montoPresupuesto() * (1 + IVA);
    }
    public int CantidadProducto()
    {
        int cantidad = 0;
        if (Detalle != null)
        {
            foreach (var item in Detalle)
            {
                cantidad += item.Cantidad;
            }
        }
        return cantidad;
    }
}