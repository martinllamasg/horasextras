
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HorasExtrasAppClean.Models;
using System.Security.Claims;

namespace HorasExtrasAppClean.Controllers
{
    [Authorize]
    public class OvertimeController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public OvertimeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OvertimeEntryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            int rowNum = 1;
            foreach (var row in model.Rows)
            {
                if (row.FileUpload != null && row.FileUpload.Length > 0)
                {
                    var fileName = $"{userId}_week{model.WeekDate:yyyyMMdd}_row{rowNum}_{Path.GetFileName(row.FileUpload.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await row.FileUpload.CopyToAsync(stream);
                    }
                }
                rowNum++;
            }

            TempData["Success"] = "Las horas extras fueron capturadas correctamente.";
            return RedirectToAction("MisRegistros");
        }

        public IActionResult MisRegistros()
        {
            // Simulación temporal
            return View(new List<string> { "Semana 1", "Semana 2", "Semana 3" });
        }

        public IActionResult Resumen()
        {
            return View();
        }
    }
}
