namespace TiendaMVC.ViewModel;

using TiendaMVC.Models;
public class PresupuestoViewModel
{
    private int id;
    private string? nombreDestinatario;
    private DateTime fechaCreacion;
    private List<PresupuestosDetalle>? detalle;

    public PresupuestoViewModel()
    {
    }

    public PresupuestoViewModel(Presupuesto presupuesto)
    {
        id = presupuesto.Id;
        nombreDestinatario = presupuesto.NombreDestinatario;
        fechaCreacion = presupuesto.FechaCreacion;
        detalle = presupuesto.Detalle;
    }

    public int Id { get => id; set => id = value; }
    public string? NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestosDetalle>? Detalle { get => detalle; set => detalle = value; }
}