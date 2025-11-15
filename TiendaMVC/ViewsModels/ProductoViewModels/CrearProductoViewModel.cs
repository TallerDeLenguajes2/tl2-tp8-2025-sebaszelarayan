using System.ComponentModel.DataAnnotations;

namespace TiendaMVC.ViewModel;

public class CrearProductoViewModel
{
    private string? descripcion;

    private decimal precio;

    [Required] [StringLength(250)] 
    public string? Descripcion { get => descripcion; set => descripcion = value; }
    [Required] [Range(0, double.MaxValue)] 
    public decimal Precio { get => precio; set => precio = value; }
}