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
	[Authorize] // Tüm endpointler için JWT zorunlu
	public class UserController : ControllerBase
	{
		private readonly TaskManagementDbContext _context;

		public UserController(TaskManagementDbContext context)
		{
			_context = context;
		}

		// GET: api/user
		[HttpGet]
		[Authorize(Roles = "Admin")] // sadece admin görebilir
		public async Task<IActionResult> GetUsers()
		{
			var users = await _context.Users
				.Select(u => new UserDTO
				{
					UserId = u.UserId,
					Name = u.Name,
					Surname = u.Surname,
					Email = u.Email,
					RoleId = u.RoleId
				})
				.ToListAsync();

			return Ok(new { success = true, data = users });
		}

		// GET: api/user/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetUser(int id)
		{
			var user = await _context.Users
				.Where(u => u.UserId == id)
				.Select(u => new UserDTO
				{
					UserId = u.UserId,
					Name = u.Name,
					Surname = u.Surname,
					Email = u.Email,
					RoleId = u.RoleId
				})
				.FirstOrDefaultAsync();

			if (user == null) return NotFound(new { success = false, message = "Kullanıcı bulunamadı" });

			return Ok(new { success = true, data = user });
		}
	}
}
