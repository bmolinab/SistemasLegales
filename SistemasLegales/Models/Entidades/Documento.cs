using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemasLegales.Models.Entidades
{
    public partial class Documento
    {
        public Documento()
        {
            Requisito = new HashSet<Requisito>();
        }

        public int IdDocumento { get; set; }

        [Required(ErrorMessage = "Debe introducir el {0}.")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "El {0} no puede tener más de {1} y menos de {2} caracteres.")]
        [Display(Name = "Requisito")]
        public string Nombre { get; set; }

        [Display(Name = "Base legal")]
        [Required(ErrorMessage = "Debe seleccionar el {0}.")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar el {0}.")]
        public int IdRequisitoLegal { get; set; }


        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Debe introducir la {0}.")]
        public int? Tipo { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Debe introducir la {0}.")]
        public int? Cantidad { get; set; }

        public virtual RequisitoLegal RequisitoLegal { get; set; }

        public virtual ICollection<Requisito> Requisito { get; set; }
    }
}
