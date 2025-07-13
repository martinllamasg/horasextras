using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace HorasExtrasAppClean.Models
{
    public class OvertimeRow
    {
        public List<string> SelectedDays { get; set; } = new();
        public Dictionary<string, double> Hours { get; set; } = new();
        public string Description { get; set; } = "";
        public IFormFile? FileUpload { get; set; }
    }
}
