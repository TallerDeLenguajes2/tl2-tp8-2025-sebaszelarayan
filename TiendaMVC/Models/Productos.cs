using TiendaMVC.ViewModel;

namespace TiendaMVC.Models;

public class Producto
{
    private int id;
    private string? descripcion;
    private decimal precio;

    public Producto()
    {
    }
    public Producto(CrearProductoViewModel productoViewModel)
    {
        descripcion = productoViewModel.Descripcion;
        precio = productoViewModel.Precio;
    }
    public Producto(EditarProductoViewModel productoViewModel)
    {
        id = productoViewModel.Id;
        descripcion = productoViewModel.Descripcion;
        precio = productoViewModel.Precio;
    }
    public decimal Precio { get => precio; set => precio = value; }
    public int Id { get => id; set => id = value; }
    public string? Descripcion { get => descripcion; set => descripcion = value; }
}