namespace TiendaMVC.Models;

public class Usuario
{
    private int id;
    private string? username;
    private string? password;
    private string? rol;
    public Usuario()
    {
    }
    public int Id { get => id; set => id = value; }
    public string? Username { get => username; set => username = value; }
    public string? Password { get => password; set => password = value; }
    public string? Rol { get => rol; set => rol = value; }
}