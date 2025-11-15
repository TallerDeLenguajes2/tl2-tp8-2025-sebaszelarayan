using System.ComponentModel.DataAnnotations;

namespace TiendaMVC.ValidationAttributes
{
    // Creamos una nueva clase de atributo que hereda de ValidationAttribute
    public class NoFutureDateAttribute : ValidationAttribute
    {
        public NoFutureDateAttribute()
        {

            ErrorMessage = "La fecha no puede ser futura.";
        }

        // Sobrescribimos el método IsValid
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true; 
            }

            if (value is DateTime fecha)
            {

                // verificamos que sea menor o igual a la fecha de hoy.
                return fecha.Date <= DateTime.Today;
            }

            // Si el tipo de dato no es DateTime, la validación falla
            return false;
        }
    }
}