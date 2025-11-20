using TiendaMVC.Models;
namespace TiendaMVC.Interface;

public interface IPresupuestoRepository
{
    void Crear(Presupuesto presupuesto);
    List<Presupuesto> GetAll();
    Presupuesto? DetallesPresupuestosID(int id);
    bool Eliminar(int id);
    void AgregarProducto(int idPresupuesto, PresupuestosDetalle detalle);
    void Modificar(Presupuesto presupuesto);

}