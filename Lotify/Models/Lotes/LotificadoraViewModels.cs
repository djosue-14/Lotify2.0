using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Lotes
{
    public class LotificadoraViewModels
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se el nombre de la lotificadora.")]
        [Display(Name = "lotificadora")]
        public string NombreLotificadora { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere la direccion de la lotificadora.")]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

    }
}