using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemasLegales.Models.Entidades
{
    public partial class Actor
    {
        public Actor()
        {
            AdminRequisitoLegalIdActorCustodioDocumento = new HashSet<Requisito>();
            AdminRequisitoLegalIdActorDuennoProceso = new HashSet<Requisito>();
            AdminRequisitoLegalIdActorResponsableGestSeg = new HashSet<Requisito>();
        }

        public int IdActor { get; set; }

        [Required(ErrorMessage = "Debe introducir los {0}.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Los {0} no pueden tener más de {1} y menos de {2} caracteres.")]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Debe introducir el {0}.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "El {0} no puede tener más de {1} y menos de {2} caracteres.")]
        [Display(Name = "Departamento")]
        public string Departamento { get; set; }

        [Required(ErrorMessage = "Debe introducir el {0}.")]
        [EmailAddress(ErrorMessage = "El {0} es inválido.")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        public virtual ICollection<Requisito> AdminRequisitoLegalIdActorCustodioDocumento { get; set; }
        public virtual ICollection<Requisito> AdminRequisitoLegalIdActorDuennoProceso { get; set; }
        public virtual ICollection<Requisito> AdminRequisitoLegalIdActorResponsableGestSeg { get; set; }
    }
}
