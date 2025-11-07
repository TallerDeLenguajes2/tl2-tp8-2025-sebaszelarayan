namespace  Tienda.Models;

public class Presupuesto
{
    const double IVA = 0.21;
    private int idPresupuesto;
    private string? nombreDestinatario;
    private DateTime fechaCreacion;
    private List<PresupuestosDetalle>? detalle;

    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public string? NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestosDetalle>? Detalle { get => detalle; set => detalle = value; }

    public Presupuesto(string? nombreDestinatario,DateTime fechaCreacion,List<PresupuestosDetalle>? detalle)
    {
        this.nombreDestinatario = nombreDestinatario;
        this.fechaCreacion = fechaCreacion;
        this.detalle = detalle;
    }
    public Presupuesto()
    {
        Detalle = new List<PresupuestosDetalle>();
    }

    public double montoPresupuesto()
    {
        double monto = 0;
        if (Detalle != null)
        {
            foreach (var item in Detalle)
            {
                if (item.Producto!=null)
                {
                    monto+=item.Producto.Precio;
                }
            }
        }
        return monto;
    }
    
        

    public double MontoPresupuestoConIva()
    {
        return montoPresupuesto()*(1+ IVA);
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