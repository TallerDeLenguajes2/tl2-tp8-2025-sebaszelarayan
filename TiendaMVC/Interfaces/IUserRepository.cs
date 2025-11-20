using TiendaMVC.Models;

namespace TiendaMVC.Interface;

public interface IUserRepository
{
    Usuario? GetUser(string username,string password);

}