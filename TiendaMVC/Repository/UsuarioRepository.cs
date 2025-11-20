using Microsoft.Data.Sqlite;
using TiendaMVC.Interface;
using TiendaMVC.Models;
namespace TiendaMVC.Repository;

public class UsuarioRepository : IUserRepository
{
    private string cadenaConnection = "Data source = Db/Tienda.db";

    public Usuario? GetUser(string username, string password)
    {
        Usuario? usuario = null;

        string query = "SELECT Id, Nombre, User, Pass, Rol FROM Usuarios WHERE User=@username AND Pass=@password " ;
        var connection = new SqliteConnection(cadenaConnection);
        connection.Open();

        var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@password", password);

        var reader = command.ExecuteReader();
        if (reader.Read())
        {
            usuario = new Usuario
            {
                //terminar la lectura
                Id = Convert.ToInt32(reader["Id"]),
                Username= reader["User"].ToString(),
                Password = reader["Pass"].ToString(),
                Rol = reader["Rol"].ToString()
            };
        }
        connection.Close();
        return usuario;
    }
}