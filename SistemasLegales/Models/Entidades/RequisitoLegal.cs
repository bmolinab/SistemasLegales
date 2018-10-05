using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemasLegales.Models.Entidades
{
    public partial class RequisitoLegal
    {
        public RequisitoLegal()
        {
            Documento = new HashSet<Documento>();
        }

        public int IdRequisitoLegal { get; set; }

        [Required(ErrorMessage = "Debe introducir el {0}.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "El {0} no puede tener más de {1} y menos de {2} caracteres.")]
        [Display(Name = "Requisito legal")]
        public string Nombre { get; set; }

        [Display(Name = "Organismo de control")]
        [Required(ErrorMessage = "Debe seleccionar el {0}.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar el {0}.")]
        public int IdOrganismoControl { get; set; }
        public virtual OrganismoControl OrganismoControl { get; set; }

        public virtual ICollection<Documento> Documento { get; set; }
    }
}
