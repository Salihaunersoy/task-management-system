using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Context;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Models;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Roles = "Admin")]
	public class ReportController : ControllerBase
	{
		private readonly TaskManagementDbContext _context;
		private readonly ExcelService            _excelService;
		private readonly WordService             _wordService;

		public ReportController(TaskManagementDbContext context, ExcelService excelService, WordService wordService)
		{
			_context      = context;
			_excelService = excelService;
			_wordService = wordService;
		}

		[HttpPost("upload")]
		public async Task<IActionResult> UploadExcel(IFormFile file)
		{
			if (file == null || file.Length == 0) return BadRequest("Dosya seçilmedi.");

			try
			{
				List<FisData> fisList = ExcelReadFisData.ReadFisData(file);

				if (fisList == null || !fisList.Any())
					return BadRequest("Dosya okundu ancak işlenecek veri bulunamadı.");

				_context.FisDatas.RemoveRange(_context.FisDatas); //çift kayıt olmasın

				await _context.FisDatas.AddRangeAsync(fisList); 

				await _context.SaveChangesAsync();

				return Ok(new
				{
					Message = "Veriler başarıyla kaydedildi.",
					Count = fisList.Count
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Sunucu hatası: {ex.Message}");
			}
		}

		[HttpGet("listFisData")]
		public async Task<IActionResult> GetFisData([FromQuery] int page = 1,[FromQuery] int pageSize = 15)
		{
			if (page < 1) page = 1;
			if (pageSize < 1 || pageSize > 100) pageSize = 15;

			int totalItems = await _context.FisDatas.CountAsync();
			int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

			List<FisData> items = await _context.FisDatas
				.AsNoTracking()
				.OrderByDescending(f => f.FisId)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return Ok(new
			{
				success = true,
				data = new
				{
					items,
					page,
					pageSize,
					totalItems,
					totalPages,
					hasPrevious = page > 1,
					hasNext = page < totalPages
				}
			});
		}

		[HttpGet("exportFisData")]
		public async Task<IActionResult> ExportFisData()
		{
			string? userName = User.Identity?.Name;
			if (string.IsNullOrEmpty(userName))
				return Unauthorized(new { success = false, message = "Invalid token or user information not found." });

			int totalCount = await _context.FisDatas.CountAsync();
			if (totalCount == 0)
				return BadRequest(new { success = false, message = "No data available for download." });

			List<FisData> fisList = await _context.FisDatas
				.AsNoTracking()
				.OrderBy(f => f.FisId)
				.ToListAsync();

			byte[] fileContent = _excelService.GenerateReport(fisList, null);

			string safeUserName = string.Join("_", userName.Split(Path.GetInvalidFileNameChars()));
			string fileName     = $"{safeUserName}_{DateTime.Now:dd-MM-yyyy_HH-mm}.xlsx";

			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			string folderPath  = Path.Combine(desktopPath, "Exports");

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			string filePath = Path.Combine(folderPath, fileName);
			await System.IO.File.WriteAllBytesAsync(filePath, fileContent);

			fisList.Clear();

			return File(fileContent, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
		}

		[HttpGet("exportFisDataAsWord")]
		public async Task<IActionResult> ExportFisDataAsWord()
		{
			string? userName = User.Identity?.Name;
			if (string.IsNullOrEmpty(userName))
				return Unauthorized(new { success = false, message = "Invalid token or user information not found." });

			int totalCount = await _context.FisDatas.CountAsync();
			if (totalCount == 0)
				return BadRequest(new { success = false, message = "No data available for download." });

			List<FisData> fisList = await _context.FisDatas
				.AsNoTracking()
				.OrderBy(f => f.FisId)
				.ToListAsync();

			byte[] fileContent = _wordService.GenerateWordReport(fisList);

			string safeUserName = string.Join("_", userName.Split(Path.GetInvalidFileNameChars()));
			string fileName = $"{safeUserName}_{DateTime.Now:dd-MM-yyyy_HH-mm}.docx";

			string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			string folderPath = Path.Combine(desktopPath, "Exports");

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			string filePath = Path.Combine(folderPath, fileName);
			await System.IO.File.WriteAllBytesAsync(filePath, fileContent);

			fisList.Clear();

			return File(fileContent, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", fileName);

		}

		[HttpGet("export")]
		public async Task<IActionResult> ExportToExcel(
			[FromQuery] bool statusChart      = false,
			[FromQuery] bool priorityChart    = false,
			[FromQuery] bool taskByUserChart  = false)
		{
			List<TaskDTO> tasks = await _context.Tasks
				.Select(t => new TaskDTO
				{
					TaskId           = t.TaskId,
					Title            = t.Title,
					Description      = t.Description,
					Status           = t.Status,
					Priority         = t.Priority,
					AssignedUserId   = t.AssignedUserId,
					CreatedByAdminId = t.CreatedByAdminId,
					DueDate          = t.DueDate
				})
				.ToListAsync();

			List<ChartRequest> charts = new List<ChartRequest>();

			if (statusChart)
			{
				charts.Add(new ChartRequest
				{
					Title = "Status",
					Type  = "doughnut",
					Data  = tasks.GroupBy(t => t.Status).ToDictionary(g => g.Key, g => g.Count())
				});
			}

			if (priorityChart)
			{
				charts.Add(new ChartRequest
				{
					Title = "Priority",
					Type  = "doughnut",
					Data  = tasks.GroupBy(t => t.Priority ?? "").ToDictionary(g => g.Key, g => g.Count())
				});
			}

			if (taskByUserChart)
			{
				List<Models.User> users = await _context.Users
					.Where(u => u.RoleId != 0)
					.ToListAsync();

				Dictionary<string, Dictionary<string, int>> barData = new();

				foreach (Models.User user in users)
				{
					string userName                     = $"{user.Name} {user.Surname}";
					List<TaskDTO> userTasks             = tasks.Where(t => t.AssignedUserId == user.UserId).ToList();
					Dictionary<string, int> statusGroup = userTasks.GroupBy(t => t.Status).ToDictionary(g => g.Key, g => g.Count());
					barData[userName]                   = statusGroup;
				}

				charts.Add(new ChartRequest
				{
					Title   = "Tasks by User",
					Type    = "bar",
					BarData = barData
				});
			}

			byte[] fileContent = _excelService.GenerateReport(
				tasks,
				charts.Count > 0 ? charts : null
			);

			string fileName = $"Rapor_{DateTime.Now:dd-MM-yyyy_HH-mm}.xlsx";

			return File(fileContent,
				"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
				fileName);
		}
	}
}
