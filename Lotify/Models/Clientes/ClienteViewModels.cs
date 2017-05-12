using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lotify.Models.Clientes
{
    public class ClienteViewModels
    {
        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el nombre del cliente.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el apellido del cliente.")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere el número de DPI del cliente.")]
        [Display(Name = "DPI")]
        public long Dpi { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere especificar el genero del cliente.")]
        [Display(Name = "Genero")]
        public string Genero { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere la dirección del cliente.")]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere la fecha de nacimiento.")]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Se requiere especificar el estado del cliente.")]
        [Display(Name = "Estado del Cliente")]
        public int EstadoClienteId { get; set; }
    }
}