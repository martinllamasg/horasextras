using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HorasExtrasAppClean.Models
{
    public class OvertimeEntryModel
    {
        [Display(Name = "Semana a reportar")]
        public DateTime? FechaSemana { get; set; }

        public int? Semana { get; set; }

        [Display(Name = "Turno")]
        [Required]
        public string Turno { get; set; } = "";

        [Display(Name = "Días seleccionados")]
        [Required]
        public List<int> DiasSeleccionados { get; set; } = new();

        [Display(Name = "Horas por día")]
        [Required]
        public List<double> HorasPorDia { get; set; } = new();

        [Required(ErrorMessage = "Debes capturar el detalle de actividades.")]
        [Display(Name = "Detalle de actividades")]
        public string DetalleActividades { get; set; } = "";

        [Display(Name = "Archivo adjunto")]
        public IFormFile? ArchivoAdjunto { get; set; }

        // NUEVO: Para guardar el usuario autenticado
        [Display(Name = "Usuario")]
        public string Usuario { get; set; } = "";
    }
}
