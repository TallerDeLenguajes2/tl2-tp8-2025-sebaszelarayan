using TiendaMVC.Models;

namespace TiendaMVC.Interface;

public interface IUsuarioRepository
{
    Usuario? GetUser(string username, string password);

}