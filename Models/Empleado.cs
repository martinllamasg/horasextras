
using System.Collections.Generic;

namespace HorasExtrasAppClean.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public List<OvertimeRecord> OvertimeRecords { get; set; } = new();
    }
}
