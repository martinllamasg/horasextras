
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HorasExtrasAppClean.Models
{
    public class OvertimeEntryModel
    {
        public DateTime WeekDate { get; set; }

        public List<OvertimeRowInput> Rows { get; set; } = new List<OvertimeRowInput>();
    }
}
