namespace TiendaMVC.ViewModel;

using System.ComponentModel.DataAnnotations;
using TiendaMVC.Models;
public class EditarProductoViewModel
{
    private int id;
    private string? descripcion;
    private decimal precio;
    public EditarProductoViewModel()
    {
    }
    public EditarProductoViewModel(Producto producto)
    {
        Id = producto.Id;
        Descripcion = producto.Descripcion;
        Precio = producto.Precio;
    }

    [Required]
    public int Id { get => id; set => id = value; }
    [Required]
    [StringLength(250)]
    public string? Descripcion { get => descripcion; set => descripcion = value; }
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Precio { get => precio; set => precio = value; }
}