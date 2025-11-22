using System.ComponentModel.DataAnnotations;

namespace  TiendaMVC.ViewModel;

public class LoginViewModel
{
        private string? username;
    private string? password;



    public LoginViewModel()
    {
    }

    [Required(ErrorMessage = "El nombre de usuario es requerido")]
    [Display(Name = "Usuario")]
    public string? Username { get => username; set => username = value; }
    [Required(ErrorMessage = "La contraseña es requerida")]
    [DataType(DataType.Password)] // Esto hace que el input sea tipo 'password' (puntitos)
    [Display(Name = "Contraseña")]
    public string? Password { get => password; set => password = value; }

}