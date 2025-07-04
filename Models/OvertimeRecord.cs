
using System;
using System.ComponentModel.DataAnnotations;

namespace HorasExtrasAppClean.Models
{
    public class OvertimeRecord
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public DateTime WeekDate { get; set; }

        [Required]
        public string Days { get; set; } = string.Empty; // Ej. "1,2"

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? FilePath { get; set; }

        public string? HoursPerDayJson { get; set; } // Ej. {"0":2,"1":3.5}

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
