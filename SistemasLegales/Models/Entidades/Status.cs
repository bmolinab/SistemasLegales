using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemasLegales.Models.Entidades
{
    public partial class Status
    {
        public Status()
        {
            AdminRequisitoLegal = new HashSet<Requisito>();
        }

        public int IdStatus { get; set; }

        [Required(ErrorMessage = "Debe introducir el {0}.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "El {0} no puede tener más de {1} y menos de {2} caracteres.")]
        [Display(Name = "Documento")]
        public string Nombre { get; set; }

        public virtual ICollection<Requisito> AdminRequisitoLegal { get; set; }
    }
}
