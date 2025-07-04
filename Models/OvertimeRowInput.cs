
using Microsoft.AspNetCore.Http;

namespace HorasExtrasAppClean.Models
{
    public class OvertimeRowInput
    {
        public List<string> SelectedDays { get; set; } = new();
        public Dictionary<string, double> HoursPerDay { get; set; } = new();
        public string Description { get; set; } = string.Empty;
        public IFormFile FileUpload { get; set; } = null!;
    }
}
