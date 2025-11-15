using Microsoft.Data.Sqlite;
using TiendaMVC.Models;
namespace TiendaMVC.Repository;

public class ProductosRepository
{
    private string cadenaConnection = "Data source = Db/Tienda.db";
    private List<Producto> _productos = new List<Producto>();

    public void Alta(Producto producto)
    {
        string query = "INSERT INTO Productos(Descripcion,Precio) VALUES (@descripcion,@precio)";
        var connection = new SqliteConnection(cadenaConnection);
        connection.Open();
        var command = new SqliteCommand(query, connection);
        command.Parameters.Add(new SqliteParameter("@descripcion", producto.Descripcion));
        command.Parameters.Add(new SqliteParameter("@precio", producto.Precio));
        command.ExecuteNonQuery();
        _productos.Add(producto);
        connection.Close();
    }
    public List<Producto> GetAll()
    {
        string query = "SELECT * FROM Productos";
        var connection = new SqliteConnection(cadenaConnection);
        connection.Open();
        var command = new SqliteCommand(query, connection);
        using SqliteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var producto = new Producto
            {
                Id = Convert.ToInt32(reader["idProducto"]),
                Descripcion = reader["descripcion"].ToString(),
                Precio = Convert.ToDecimal(reader["precio"])
            };
            _productos.Add(producto);
        }
        connection.Close();
        return _productos;
    }
    public Producto? DetallesProductosID(int id)
    {
        Producto? producto = null;

        string query = "SELECT * FROM Productos WHERE idProducto=@id";
        var connection = new SqliteConnection(cadenaConnection);
        connection.Open();

        var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);


        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            producto = new Producto
            {
                Id = Convert.ToInt32(reader["idProducto"]),
                Descripcion = reader["descripcion"].ToString(),
                Precio = Convert.ToDecimal(reader["precio"])
            };
        }
        connection.Close();
        return producto;
    }
    public void Modificar(Producto producto)
    {

        using var connection = new SqliteConnection(cadenaConnection);
        connection.Open();
        string sql = "UPDATE Productos SET Descripcion=@descripcion,Precio=@precio WHERE idProducto = @id";
        using var command = new SqliteCommand(sql, connection);

        command.Parameters.Add(new SqliteParameter("@descripcion", producto.Descripcion));
        command.Parameters.Add(new SqliteParameter("@precio", producto.Precio));
        command.Parameters.Add(new SqliteParameter("@id", producto.Id));

        command.ExecuteNonQuery();
        connection.Close();
    }

    public bool Eliminar(int id)
    {
        using var connection = new SqliteConnection(cadenaConnection);
        connection.Open();
        string sql = "DELETE FROM Productos WHERE idProducto = @id";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.Add(new SqliteParameter("@id", id));
        command.ExecuteNonQuery();
        connection.Close();
        return true;
    }

}

