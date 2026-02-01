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
	public class UserController : ControllerBase
	{
		private readonly TaskManagementDbContext _context;

		public UserController(TaskManagementDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Authorize(Roles = "Admin")]  
		public async Task<IActionResult> GetUsers()
		{
			List<UserDTO> users = await _context.Users
				.Select(u => new UserDTO
				{
					UserId   = u.UserId,
					Name     = u.Name,
					Surname  = u.Surname,
					Email    = u.Email,
					RoleId   = u.RoleId
				})
				.ToListAsync();

			return Ok(new { success = true, data = users });
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUser(int id)
		{
			UserDTO? user = await _context.Users
				.Where(u => u.UserId == id)
				.Select(u => new UserDTO
				{
					UserId  = u.UserId,
					Name    = u.Name,
					Surname = u.Surname,
					Email   = u.Email,
					RoleId  = u.RoleId
				})
				.FirstOrDefaultAsync();

			if (user == null) return NotFound(new { success = false, message = "Kullanıcı bulunamadı" });

			return Ok(new { success = true, data = user });
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto)
		{
			var exists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
			if (exists) return BadRequest(new { success = false, message = "Bu e-posta zaten kayıtlı" });

			User user = new User
			{
				Name          = dto.Name,
				Surname       = dto.Surname,
				Email         = dto.Email,
				PasswordHash  = dto.Password,
				RoleId        = dto.RoleId,
				UserCreatedAt = DateTime.Now
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return Ok(new { success = true, data = new UserDTO
			{
				UserId  = user.UserId,
				Name    = user.Name,
				Surname = user.Surname,
				Email   = user.Email,
				RoleId  = user.RoleId
			}});
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateUser(int id, [FromBody] CreateUserDTO dto)
		{
			User? user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound(new { success = false, message = "Kullanıcı bulunamadı" });

			user.Name    = dto.Name;
			user.Surname = dto.Surname;
			user.Email   = dto.Email;
			user.RoleId  = dto.RoleId;

			if (!string.IsNullOrEmpty(dto.Password))
				user.PasswordHash = dto.Password;

			await _context.SaveChangesAsync();

			return Ok(new { success = true, data = new UserDTO
			{
				UserId  = user.UserId,
				Name    = user.Name,
				Surname = user.Surname,
				Email   = user.Email,
				RoleId  = user.RoleId
			}});
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			User? user = await _context.Users.FindAsync(id);
			if (user == null) return NotFound(new { success = false, message = "Kullanıcı bulunamadı" });

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();

			return Ok(new { success = true, message = "Kullanıcı silindi" });
		}
	}
}
