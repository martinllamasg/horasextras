using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HorasExtrasAppClean.Models
{[Table("OvertimeRecord")]
    public class OvertimeRecord
    {
        public int Id { get; set; }

        [Required]
        public string Estatus { get; set; } = "Guardado";

        [Required]
        public bool Enviado { get; set; } = false;

        [Required]
        public bool Rechazado { get; set; } = false;

        [Required]
        public string UserId { get; set; } = "";

        [Required]
        public string NombreEmpleado { get; set; } = "";

        [Required]
        public int WeekNumber { get; set; }

        [Required]
        public string Turno { get; set; } = "";

        [Required]
        public DateTime WeekDate { get; set; }

        [Required]
        public string DiasSeleccionadosJson { get; set; } = "[]";

        [Required]
        public string HorasPorDiaJson { get; set; } = "[]";

        public string? DetalleActividades { get; set; }

        public string? FilePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // NUEVO: Propiedad para el motivo del rechazo
        public string? RazonRechazo { get; set; }
    }
}
