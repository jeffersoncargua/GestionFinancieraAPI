using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestionFinancieraNet.Models.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [RegularExpression(@"@^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Debe ser un correo valido)")]
        [EmailAddress]
        public string Email { get; set; }

        [PasswordPropertyText]
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }
    }
}
