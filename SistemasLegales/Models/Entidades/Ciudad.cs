using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemasLegales.Models.Entidades
{
    public partial class Ciudad
    {
        public Ciudad()
        {
            AdminRequisitoLegal = new HashSet<Requisito>();
        }

        public int IdCiudad { get; set; }

        [Required(ErrorMessage = "Debe introducir la {0}.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "La {0} no puede tener más de {1} y menos de {2} caracteres.")]
        [Display(Name = "Ciudad")]
        public string Nombre { get; set; }

        public virtual ICollection<Requisito> AdminRequisitoLegal { get; set; }
    }
}
