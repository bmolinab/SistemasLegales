using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemasLegales.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Debe introducir la {0}.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La {0} no puede tener más de {1} y menos de {2} caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña anterior")]
        public string PasswordAnterior { get; set; }

        [Required(ErrorMessage = "Debe introducir la {0}.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La {0} no puede tener más de {1} y menos de {2} caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe introducir la {0}.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La {0} no puede tener más de {1} y menos de {2} caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y su confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
