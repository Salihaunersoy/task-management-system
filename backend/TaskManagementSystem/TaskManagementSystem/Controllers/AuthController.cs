using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.Context;
using TaskManagementSystem.DTOs;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly TaskManagementDbContext _context;
		private readonly IConfiguration _config;

		public AuthController(TaskManagementDbContext context, IConfiguration config)
		{
			_context = context;
			_config = config;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDTO dto)
		{
			User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

			if (user == null)
				return Unauthorized(new { success = false, message = "Kullanıcı bulunamadı." });

			if (dto.Password != user.PasswordHash)
				return Unauthorized(new { success = false, message = "E-posta veya şifre hatalı." });

			string token = GenerateJwtToken(user);

			LoginResponseDTO response = new LoginResponseDTO
			{
				Token   = token,
				UserId  = user.UserId,
				Name    = user.Name,
				Surname = user.Surname,
				Email   = user.Email,
				RoleId  = user.RoleId
			};

			return Ok(new { success = true, data = response });
		}

		private string GenerateJwtToken(User user)
		{
			var key     = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtConfig:Key"]!));
			var creds   = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var expires = DateTime.Now.AddMinutes(double.Parse(_config["JwtConfig:ExpirationInMinutes"]!));

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Name, $"{user.Name} {user.Surname}"),
				new Claim(ClaimTypes.Role, user.RoleId == 0 ? "Admin" : "User")
			};

			var token = new JwtSecurityToken(
				issuer:   _config["JwtConfig:Issuer"],
				audience: _config["JwtConfig:Audience"],
				claims:   claims,
				expires:  expires,
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
