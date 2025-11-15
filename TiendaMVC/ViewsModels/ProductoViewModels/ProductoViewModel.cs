using TiendaMVC.Models;

namespace TiendaMVC.ViewModel;

public class ProductoViewModel
{
    private int id;
    private string? descripcion;
    private decimal precio;

    public ProductoViewModel(Producto producto)
    {
        id = producto.Id;
        descripcion = producto.Descripcion;
        precio = producto.Precio;
    }
    public ProductoViewModel()
    {
    }
    public int Id { get => id; set => id = value; }
    public string? Descripcion { get => descripcion; set => descripcion = value; }
    public decimal Precio { get => precio; set => precio = value; }
}