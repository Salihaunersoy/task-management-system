using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Context;
using TaskManagementSystem.DTOs;
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

		public ReportController(TaskManagementDbContext context, ExcelService excelService)
		{
			_context      = context;
			_excelService = excelService;
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
