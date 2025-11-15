using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TiendaMVC.ViewModel;
public class AgregarProductoViewModel
{
    private int idPresupuesto;
    private int idProducto;
    private int cantidad;
    private SelectList? listaProductos;

    public AgregarProductoViewModel()
    {
        
    }
    public AgregarProductoViewModel(int id,SelectList listaProductos)
    {
        idPresupuesto=id;
        this.listaProductos=listaProductos;
    }
    [Required]
    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public int IdProducto { get => idProducto; set => idProducto = value; }
    public SelectList? ListaProductos { get => listaProductos; set => listaProductos = value; }
    [Required]
    [Range(1,int.MaxValue)]
    public int Cantidad { get => cantidad; set => cantidad = value; }
}