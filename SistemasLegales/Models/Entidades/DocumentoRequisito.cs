using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemasLegales.Models.Entidades
{
    public partial class DocumentoRequisito
    {
        public int IdDocumentoRequisito { get; set; }

        [Required(ErrorMessage = "Debe introducir el {0}.")]
        [Display(Name = "Documento")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "El {0} no puede tener más de {1} y menos de {2} caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe introducir la {0}.")]
        [Display(Name = "Fecha de subida")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Debe introducir la {0}.")]
        [Display(Name = "Dirección")]
        [StringLength(1024, MinimumLength = 2, ErrorMessage = "La {0} no puede tener más de {1} y menos de {2} caracteres.")]
        public string Url { get; set; }

        [Display(Name = "Requisito")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar el {0}.")]
        public int? IdRequisito { get; set; }
        public virtual Requisito Requisito { get; set; }
    }
}
