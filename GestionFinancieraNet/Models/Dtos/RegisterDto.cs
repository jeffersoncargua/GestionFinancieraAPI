using GestionFinancieraNet.Models.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestionFinancieraNet.Models.Dtos
{
    public class RegisterDto 
    {
        [Required(ErrorMessage = "El campo del nombre es requerido")]
        [RegularExpression(@"^[a-zA-Z\s]{1,30}$", ErrorMessage = "Debe tener letras y espacios(de ser necesario)")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "El campo del password es requerido")]
        [PasswordPropertyText]
        public required string Password { get; set; }

        [Required(ErrorMessage = "El campo del email es requerido")]
        [RegularExpression(@"@^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Debe ser un correo valido)")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "El campo del telefono es requerido")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Debe tener numeros de hasta 10 digitos")]
        public required string Phone { get; set; }

        public string? Role { get; set; }
    }
}

