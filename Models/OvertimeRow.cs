
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorasExtrasAppClean.Models
{
    public class OvertimeRow
    {
        [Key]
        public int Id { get; set; }

        public int? Lunes { get; set; }
        public int? Martes { get; set; }
        public int? Miércoles { get; set; }
        public int? Jueves { get; set; }
        public int? Viernes { get; set; }
        public int? Sábado { get; set; }
        public int? Domingo { get; set; }

        [MaxLength(100)]
        public string Descripcion { get; set; } = string.Empty;

        [ForeignKey("OvertimeEntry")]
        public int OvertimeEntryId { get; set; }

        public OvertimeEntryModel OvertimeEntry { get; set; } = default!;
    }
}
