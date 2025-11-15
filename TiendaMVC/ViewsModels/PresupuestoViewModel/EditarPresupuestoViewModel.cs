namespace TiendaMVC.ViewModel;

using System.ComponentModel.DataAnnotations;
using TiendaMVC.Models;
using TiendaMVC.ValidationAttributes;
public class EditarPresupuestoViewModel
{
    private int id;
    private string? nombreDestinatario;
    private DateTime fechaCreacion;
    private List<PresupuestosDetalle>? detalle;

    public EditarPresupuestoViewModel()
    {
        fechaCreacion=DateTime.Today;
    }
    public EditarPresupuestoViewModel(Presupuesto presupuesto)
    {
        id = presupuesto.Id;
        nombreDestinatario = presupuesto.NombreDestinatario;
        fechaCreacion = presupuesto.FechaCreacion;
        detalle = presupuesto.Detalle;
    }

    [Required]
    public int Id { get => id; set => id = value; }
    [Required]
    public string? NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Creación")] 
    [NoFutureDate] 
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestosDetalle>? Detalle { get => detalle; set => detalle = value; }
}