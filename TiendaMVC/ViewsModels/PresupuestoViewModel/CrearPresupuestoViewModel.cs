using TiendaMVC.Models;

namespace TiendaMVC.ViewModel;

using System.ComponentModel.DataAnnotations;
using TiendaMVC.ValidationAttributes;
public class CrearPresupuestoViewModel
{

    
    private string? nombreDestinatario;
    private DateTime fechaCreacion;
    private List<PresupuestosDetalle>? detalle;

[Required]
    public string? NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Creación")]
    [NoFutureDate]
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestosDetalle>? Detalle { get => detalle; set => detalle = value; }
    
}
