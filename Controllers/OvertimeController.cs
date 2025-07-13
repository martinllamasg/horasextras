using HorasExtrasAppClean.Data;
using HorasExtrasAppClean.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HorasExtrasAppClean.Controllers
{
    public class OvertimeController : Controller
    {
        private readonly OvertimeDbContext _db;
        private readonly IWebHostEnvironment _environment;

        public OvertimeController(OvertimeDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult MisRegistros()
        {
            var userId = User.Identity?.Name ?? "demo";
            var registros = _db.OvertimeRecords.Where(r => r.UserId == userId).ToList();
            return View(registros);
        }

        public IActionResult Create()
        {
            var modelo = new List<OvertimeEntryModel> { new OvertimeEntryModel() };
            ViewBag.SemanaActual = GetCurrentWeekNumber();
            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<OvertimeEntryModel> model)
        {
            if (model == null || model.Count == 0)
            {
                ModelState.AddModelError("", "Debes capturar al menos un registro.");
                ViewBag.SemanaActual = GetCurrentWeekNumber();
                return View(model ?? new List<OvertimeEntryModel> { new OvertimeEntryModel() });
            }
            if (model.Count > 7)
            {
                ModelState.AddModelError("", "Solo puedes capturar hasta 7 registros por semana.");
                ViewBag.SemanaActual = GetCurrentWeekNumber();
                return View(model);
            }

            var ci = CultureInfo.CurrentCulture;
            var userId = User.Identity?.Name ?? "demo";
            var nombresDias = new[] { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };

            // Validar que la fecha de la semana no sea null antes de usar
            if (!model[0].FechaSemana.HasValue)
            {
                ModelState.AddModelError("[0].FechaSemana", "Selecciona la fecha de la semana.");
                ViewBag.SemanaActual = GetCurrentWeekNumber();
                return View(model);
            }

            int weekNum = ci.Calendar.GetWeekOfYear(
                model[0].FechaSemana.Value,
                ci.DateTimeFormat.CalendarWeekRule,
                ci.DateTimeFormat.FirstDayOfWeek
            );

            int registrosPrevios = _db.OvertimeRecords
                .Where(r => r.UserId == userId && r.WeekNumber == weekNum)
                .Count();

            if (registrosPrevios + model.Count > 7)
            {
                ModelState.AddModelError("", $"Solo puedes capturar hasta 7 registros por semana. Ya tienes {registrosPrevios} guardados y estás intentando agregar {model.Count} más.");
                ViewBag.SemanaActual = GetCurrentWeekNumber();
                return View(model);
            }

            var diasYaReportados = new HashSet<int>();
            var registrosPreviosList = _db.OvertimeRecords
                .Where(r => r.UserId == userId && r.WeekNumber == weekNum)
                .ToList();

            foreach (var previo in registrosPreviosList)
            {
                if (!string.IsNullOrWhiteSpace(previo.DiasSeleccionadosJson))
                {
                    var diasPrevios = System.Text.Json.JsonSerializer.Deserialize<List<int>>(previo.DiasSeleccionadosJson);
                    foreach (var dia in diasPrevios ?? new List<int>())
                        diasYaReportados.Add(dia);
                }
            }

            var diasCapturaActual = new HashSet<int>();

            for (int i = 0; i < model.Count; i++)
            {
                var registro = model[i];

                if (i == 0)
                {
                    if (!registro.FechaSemana.HasValue)
                        ModelState.AddModelError($"[{i}].FechaSemana", "Selecciona la fecha de la semana.");
                    if (string.IsNullOrWhiteSpace(registro.Turno))
                        ModelState.AddModelError($"[{i}].Turno", "Selecciona el turno.");
                }

                if (registro.DiasSeleccionados == null || registro.DiasSeleccionados.Count == 0)
                {
                    ModelState.AddModelError($"[{i}].DiasSeleccionados", "Debes seleccionar al menos un día.");
                }
                else
                {
                    if (registro.DiasSeleccionados.Count > 2)
                        ModelState.AddModelError($"[{i}].DiasSeleccionados", "Solo puedes seleccionar hasta 2 días.");
                    if (registro.DiasSeleccionados.Count == 2)
                    {
                        var diff = Math.Abs(registro.DiasSeleccionados[0] - registro.DiasSeleccionados[1]);
                        if (diff != 1)
                            ModelState.AddModelError($"[{i}].DiasSeleccionados", "Solo puedes seleccionar días consecutivos.");
                    }

                    var diasDuplicadosBD = registro.DiasSeleccionados.Where(d => diasYaReportados.Contains(d)).ToList();
                    if (diasDuplicadosBD.Any())
                    {
                        ModelState.AddModelError($"[{i}].DiasSeleccionados", $"Ya registraste los días: {string.Join(", ", diasDuplicadosBD.Select(d => nombresDias[d]))} para la semana seleccionada.");
                    }

                    foreach (var dia in registro.DiasSeleccionados)
                    {
                        if (diasCapturaActual.Contains(dia))
                        {
                            ModelState.AddModelError($"[{i}].DiasSeleccionados", $"El día '{nombresDias[dia]}' ya ha sido seleccionado en otro registro de esta captura.");
                        }
                        diasCapturaActual.Add(dia);
                    }
                }

                if (registro.HorasPorDia == null || registro.HorasPorDia.Count == 0)
                {
                    ModelState.AddModelError($"[{i}].HorasPorDia", "Debes capturar las horas trabajadas.");
                }
                else
                {
                    var horasCapturadas = registro.HorasPorDia.Where(h => h > 0).ToList();
                    if (horasCapturadas.Count == 0)
                        ModelState.AddModelError($"[{i}].HorasPorDia", "Debes capturar al menos una hora en el(los) día(s) seleccionado(s).");
                    foreach (var h in horasCapturadas)
                    {
                        if (h < 0.5 || h > 10)
                            ModelState.AddModelError($"[{i}].HorasPorDia", "Las horas deben ser entre 0.5 y 10 por día.");
                    }
                }

                if (string.IsNullOrWhiteSpace(registro.DetalleActividades))
                    ModelState.AddModelError($"[{i}].DetalleActividades", "Debes escribir el detalle de las actividades.");

                if (registro.ArchivoAdjunto == null || registro.ArchivoAdjunto.Length == 0)
                {
                    ModelState.AddModelError($"[{i}].ArchivoAdjunto", "Debes adjuntar un archivo PDF o JPG.");
                }
                else
                {
                    var ext = Path.GetExtension(registro.ArchivoAdjunto.FileName).ToLowerInvariant();
                    if (ext != ".pdf" && ext != ".jpg" && ext != ".jpeg")
                        ModelState.AddModelError($"[{i}].ArchivoAdjunto", "Solo se permiten archivos PDF o JPG.");
                    if (registro.ArchivoAdjunto.Length > 4 * 1024 * 1024)
                        ModelState.AddModelError($"[{i}].ArchivoAdjunto", "El archivo no debe pesar más de 4MB.");
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.SemanaActual = GetCurrentWeekNumber();
                return View(model);
            }

            var uploadsFolder = Path.Combine(_environment.WebRootPath ?? string.Empty, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            int rowNum = 1;
            foreach (var registro in model)
            {
                string? filePath = null;
                if (registro.ArchivoAdjunto != null && registro.ArchivoAdjunto.Length > 0)
                {
                    var safeDate = registro.FechaSemana?.ToString("yyyyMMdd") ?? DateTime.UtcNow.ToString("yyyyMMdd");
                    var fileName = $"{userId}_week{safeDate}_row{rowNum}_{Path.GetFileName(registro.ArchivoAdjunto.FileName)}";
                    filePath = Path.Combine("uploads", fileName);
                    using (var stream = new FileStream(Path.Combine(_environment.WebRootPath ?? string.Empty, filePath), FileMode.Create))
                    {
                        await registro.ArchivoAdjunto.CopyToAsync(stream);
                    }
                }

                DateTime fecha = registro.FechaSemana ?? DateTime.Now;
                int weekNumber = ci.Calendar.GetWeekOfYear(
                    fecha,
                    ci.DateTimeFormat.CalendarWeekRule,
                    ci.DateTimeFormat.FirstDayOfWeek
                );

                var overtimeRecord = new OvertimeRecord
                {
                    UserId = userId,
                    NombreEmpleado = userId, // Cambiar si se tiene nombre real
                    WeekNumber = weekNumber,
                    Turno = registro.Turno ?? string.Empty,
                    WeekDate = fecha.Date,
                    DiasSeleccionadosJson = System.Text.Json.JsonSerializer.Serialize(registro.DiasSeleccionados ?? new List<int>()),
                    HorasPorDiaJson = System.Text.Json.JsonSerializer.Serialize(registro.HorasPorDia ?? new List<double>()),
                    DetalleActividades = registro.DetalleActividades ?? string.Empty,
                    FilePath = filePath ?? string.Empty,
                    CreatedAt = DateTime.UtcNow,
                    Estatus = "Guardado",
                    Enviado = false,
                    Rechazado = false,
                    RazonRechazo = null
                };

                _db.OvertimeRecords.Add(overtimeRecord);
                rowNum++;
            }

            await _db.SaveChangesAsync();

            TempData["Mensaje"] = "Tus registros de horas extra se guardaron correctamente.";
            return RedirectToAction("MisRegistros");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnviarAutorizacion(int semana)
        {
            var userId = User.Identity?.Name ?? "demo";

            var registros = _db.OvertimeRecords
                .Where(r => r.UserId == userId && r.WeekNumber == semana && !r.Enviado)
                .ToList();

            foreach (var registro in registros)
            {
                registro.Enviado = true;
                registro.Estatus = "Enviado";
                _db.OvertimeRecords.Update(registro);
            }

            await _db.SaveChangesAsync();

            TempData["Mensaje"] = $"Registros de la semana {semana} enviados a autorización correctamente.";
            return RedirectToAction("MisRegistros");
        }

        private int GetCurrentWeekNumber()
        {
            var fecha = DateTime.Now;
            var cal = CultureInfo.CurrentCulture.Calendar;
            return cal.GetWeekOfYear(fecha, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}

