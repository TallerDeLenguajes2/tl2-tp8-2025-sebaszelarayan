using TiendaMVC.Models;

namespace TiendaMVC.Interface;

public interface IProductoRepository
{
    void Alta(Producto producto);
    List<Producto> GetAll();

    Producto? DetallesProductosID(int id);

    void Modificar(Producto producto);

    bool Eliminar(int id);

}
