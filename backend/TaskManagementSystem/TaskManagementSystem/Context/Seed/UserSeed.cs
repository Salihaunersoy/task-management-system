using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Context.Seed
{
	public static class UserSeed
	{
		public static void SeedUsers(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(
				
				// Adminler (RoleId = 0)
				new User
				{
					UserId = 1,
					RoleId = 0,
					Name = "Saliha",
					Surname = "Ünersoy",
					Email = "admin@company.com",
					PasswordHash = "admin123",
					UserCreatedAt = new DateTime(2026, 1, 1, 10, 0, 0)
				},
				new User
				{
					UserId = 2,
					RoleId = 0,
					Name = "Admin",
					Surname = "Şen",
					Email = "admin2@company.com",
					PasswordHash = "admin123",
					UserCreatedAt = new DateTime(2026, 1, 2, 9, 0, 0)
				},

				// Kullanıcılar (RoleId = 1)
				new User
				{
					UserId = 3,
					RoleId = 1,
					Name = "Ahmet",
					Surname = "Yılmaz",
					Email = "ahmet.yilmaz@company.com",
					PasswordHash = "ahmet123",
					UserCreatedAt = new DateTime(2026, 1, 5, 9, 30, 0)
				},
				new User
				{
					UserId = 4,
					RoleId = 1,
					Name = "Ayşe",
					Surname = "Kaya",
					Email = "ayse.kaya@company.com",
					PasswordHash = "ayse123",
					UserCreatedAt = new DateTime(2026, 1, 6, 10, 15, 0)
				},
				new User
				{
					UserId = 5,
					RoleId = 1,
					Name = "Mehmet",
					Surname = "Demir",
					Email = "mehmet.demir@company.com",
					PasswordHash = "mehmet123",
					UserCreatedAt = new DateTime(2026, 1, 7, 11, 0, 0)
				},
				new User
				{
					UserId = 6,
					RoleId = 1,
					Name = "Fatma",
					Surname = "Şahin",
					Email = "fatma.sahin@company.com",
					PasswordHash = "fatma123",
					UserCreatedAt = new DateTime(2026, 1, 8, 14, 30, 0)
				},
				new User
				{
					UserId = 7,
					RoleId = 1,
					Name = "Ali",
					Surname = "Çelik",
					Email = "ali.celik@company.com",
					PasswordHash = "ali123",
					UserCreatedAt = new DateTime(2026, 1, 9, 8, 45, 0)
				},
				new User
				{
					UserId = 8,
					RoleId = 1,
					Name = "Zeynep",
					Surname = "Aydın",
					Email = "zeynep.aydin@company.com",
					PasswordHash = "zeynep123",
					UserCreatedAt = new DateTime(2026, 1, 10, 13, 20, 0)
				},
				new User
				{
					UserId = 9,
					RoleId = 1,
					Name = "Mustafa",
					Surname = "Koç",
					Email = "mustafa.koc@company.com",
					PasswordHash = "mustafa123",
					UserCreatedAt = new DateTime(2026, 1, 11, 15, 10, 0)
				},
				new User
				{
					UserId = 10,
					RoleId = 1,
					Name = "Elif",
					Surname = "Arslan",
					Email = "elif.arslan@company.com",
					PasswordHash = "elif123",
					UserCreatedAt = new DateTime(2026, 1, 12, 9, 50, 0)
				},
				new User
				{
					UserId = 11,
					RoleId = 1,
					Name = "Can",
					Surname = "Öztürk",
					Email = "can.ozturk@company.com",
					PasswordHash = "can123",
					UserCreatedAt = new DateTime(2026, 1, 13, 10, 30, 0)
				},
				new User
				{
					UserId = 12,
					RoleId = 1,
					Name = "Selin",
					Surname = "Kurt",
					Email = "selin.kurt@company.com",
					PasswordHash = "selin123",
					UserCreatedAt = new DateTime(2026, 1, 14, 12, 0, 0)
				}
			);
		}
	}
}
