namespace  TiendaMVC.Models;
public class PresupuestosDetalle
{
    private Producto? producto;
    private int cantidad;

    public PresupuestosDetalle()
    {
        cantidad = 0;
        producto = new Producto();
    }
    public PresupuestosDetalle(Producto producto,int cantidad)
    {
        this.cantidad = cantidad;
        this.producto = producto;
    }

    public Producto? Producto { get => producto; set => producto = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
}