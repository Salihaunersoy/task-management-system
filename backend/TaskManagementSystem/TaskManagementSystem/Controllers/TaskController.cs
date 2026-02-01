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
			List<TaskDTO> tasks = await _context.Tasks
				.Select(t => new TaskDTO
				{
					TaskId			 = t.TaskId,
					Title			 = t.Title,
					Description		 = t.Description,
					Status			 = t.Status,
					AssignedUserId   = t.AssignedUserId,
					CreatedByAdminId = t.CreatedByAdminId,
					DueDate			 = t.DueDate,
					Priority		 = t.Priority
				})
				.ToListAsync();

			return Ok(new { success = true, data = tasks });
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetTask(int id)
		{
			TaskDTO? task = await _context.Tasks
				.Where(t => t.TaskId == id)
				.Select(t => new TaskDTO
				{
					TaskId			 = t.TaskId,
					Title			 = t.Title,
					Description		 = t.Description,
					Status			 = t.Status,
					AssignedUserId   = t.AssignedUserId,
					CreatedByAdminId = t.CreatedByAdminId,
					DueDate			 = t.DueDate,
					Priority		 = t.Priority
				})
				.FirstOrDefaultAsync();

			if (task == null) return NotFound(new { success = false, message = "Görev bulunamadı" });

			return Ok(new { success = true, data = task });
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateTask([FromBody] TaskDTO dto)
		{
			Models.Task task = new Models.Task
			{
				Title            = dto.Title,
				Description		 = dto.Description,
				Status			 = dto.Status ?? "ToDo",
				Priority		 = dto.Priority ?? "Medium",
				AssignedUserId   = dto.AssignedUserId,
				CreatedByAdminId = dto.CreatedByAdminId,
				DueDate          = dto.DueDate,
				TaskCreatedAt    = DateTime.Now
			};

			_context.Tasks.Add(task);
			await _context.SaveChangesAsync();

			return Ok(new { success = true, data = task });
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDTO dto)
		{
			Models.Task? task = await _context.Tasks.FindAsync(id);
			if (task == null) return NotFound(new { success = false, message = "Görev bulunamadı" });

			task.Title            = dto.Title;
			task.Description      = dto.Description;
			task.Status           = dto.Status ?? task.Status;
			task.Priority         = dto.Priority ?? task.Priority;
			task.AssignedUserId   = dto.AssignedUserId;
			task.CreatedByAdminId = dto.CreatedByAdminId;
			task.DueDate          = dto.DueDate;

			await _context.SaveChangesAsync();
			return Ok(new { success = true, data = task });
		}

		[HttpPut("{id}/status")]
		public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] TaskStatusDTO dto)
		{
			Models.Task? task = await _context.Tasks.FindAsync(id);
			if (task == null) return NotFound(new { success = false, message = "Görev bulunamadı" });

			task.Status = dto.Status;
			await _context.SaveChangesAsync();

			return Ok(new { success = true, data = task });
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteTask(int id)
		{
			Models.Task? task = await _context.Tasks.FindAsync(id);
			if (task == null) return NotFound(new { success = false, message = "Görev bulunamadı" });

			_context.Tasks.Remove(task);
			await _context.SaveChangesAsync();
			return Ok(new { success = true, message = "Görev silindi" });
		}
	}
}
