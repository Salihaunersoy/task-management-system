using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Context;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class TaskController : ControllerBase
	{
		private readonly TaskManagementDbContext _context;

		public TaskController(TaskManagementDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllTasks()
		{
			var tasks = await _context.Tasks
				.Include(t => t.AssignedUserId)
				.Select(t => new TaskDTO
				{
					TaskId			 = t.TaskId,
					Title			 = t.Title,
					Description		 = t.Description,
					Status			 = t.Status,
					AssignedUserId   = t.AssignedUserId,
					CreatedByAdminId = t.CreatedByAdminId,
					DueDate			 = t.DueDate
				})
				.ToListAsync();

			return Ok(new { success = true, data = tasks });
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetTask(int id)
		{
			var task = await _context.Tasks
				.Where(t => t.TaskId == id)
				.Select(t => new TaskDTO
				{
					TaskId			 = t.TaskId,
					Title			 = t.Title,
					Description		 = t.Description,
					Status			 = t.Status,
					AssignedUserId   = t.AssignedUserId,
					CreatedByAdminId = t.CreatedByAdminId,
					DueDate			 = t.DueDate
				})
				.FirstOrDefaultAsync();

			if (task == null) return NotFound(new { success = false, message = "Görev bulunamadı" });

			return Ok(new { success = true, data = task });
		}

		[HttpPost]
		[Authorize(Roles = "Admin")] // sadece admin ekleyebilir
		public async Task<IActionResult> CreateTask([FromBody] TaskDTO dto)
		{
			var task = new Models.Task
			{
				Title = dto.Title,
				Description = dto.Description,
				Status = dto.Status ?? "ToDo",
				AssignedUserId = dto.AssignedUserId,
				CreatedByAdminId = dto.CreatedByAdminId,
				DueDate = dto.DueDate,
				TaskCreatedAt = DateTime.Now
			};

			_context.Tasks.Add(task);
			await _context.SaveChangesAsync();

			return Ok(new { success = true, data = task });
		}
	}
}
