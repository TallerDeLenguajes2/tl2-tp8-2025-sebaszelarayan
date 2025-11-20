namespace TiendaMVC.Interface;

public interface IAuthenticationService
{
    bool Login(string username,string password);
    void Logout();
    bool IsAutenticated();

    bool HasAccessLevel(string requiredAccessLevel);
}