using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Data.Seed
{
	public static class UserSeed
	{
		public static void SeedUsers(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(
				// Adminler (RoleId = 1)
				new User
				{
					UserId = 1,
					RoleId = 1,
					Name = "Admin",
					Surname = "Yönetici",
					Email = "admin@taskmanagement.com",
					PasswordHash = "TempHash_Admin123",
					UserCreatedAt = new DateTime(2024, 1, 1, 10, 0, 0)
				},
				new User
				{
					UserId = 2,
					RoleId = 1,
					Name = "Süper",
					Surname = "Admin",
					Email = "superadmin@taskmanagement.com",
					PasswordHash = "TempHash_SuperAdmin123",
					UserCreatedAt = new DateTime(2024, 1, 2, 9, 0, 0)
				},

				// Kullanıcılar (RoleId = 2)
				new User
				{
					UserId = 3,
					RoleId = 2,
					Name = "Ahmet",
					Surname = "Yılmaz",
					Email = "ahmet.yilmaz@company.com",
					PasswordHash = "TempHash_User123",
					UserCreatedAt = new DateTime(2024, 1, 5, 9, 30, 0)
				},
				new User
				{
					UserId = 4,
					RoleId = 2,
					Name = "Ayşe",
					Surname = "Kaya",
					Email = "ayse.kaya@company.com",
					PasswordHash = "TempHash_User123",
					UserCreatedAt = new DateTime(2024, 1, 6, 10, 15, 0)
				},
				new User
				{
					UserId = 5,
					RoleId = 2,
					Name = "Mehmet",
					Surname = "Demir",
					Email = "mehmet.demir@company.com",
					PasswordHash = "TempHash_User123",
					UserCreatedAt = new DateTime(2024, 1, 7, 11, 0, 0)
				},
				new User
				{
					UserId = 6,
					RoleId = 2,
					Name = "Fatma",
					Surname = "Şahin",
					Email = "fatma.sahin@company.com",
					PasswordHash = "TempHash_User123",
					UserCreatedAt = new DateTime(2024, 1, 8, 14, 30, 0)
				},
				new User
				{
					UserId = 7,
					RoleId = 2,
					Name = "Ali",
					Surname = "Çelik",
					Email = "ali.celik@company.com",
					PasswordHash = "TempHash_User123",
					UserCreatedAt = new DateTime(2024, 1, 9, 8, 45, 0)
				},
				new User
				{
					UserId = 8,
					RoleId = 2,
					Name = "Zeynep",
					Surname = "Aydın",
					Email = "zeynep.aydin@company.com",
					PasswordHash = "TempHash_User123",
					UserCreatedAt = new DateTime(2024, 1, 10, 13, 20, 0)
				},
				new User
				{
					UserId = 9,
					RoleId = 2,
					Name = "Mustafa",
					Surname = "Koç",
					Email = "mustafa.koc@company.com",
					PasswordHash = "TempHash_User123",
					UserCreatedAt = new DateTime(2024, 1, 11, 15, 10, 0)
				},
				new User
				{
					UserId = 10,
					RoleId = 2,
					Name = "Elif",
					Surname = "Arslan",
					Email = "elif.arslan@company.com",
					PasswordHash = "TempHash_User123",
					UserCreatedAt = new DateTime(2024, 1, 12, 9, 50, 0)
				},
				new User
				{
					UserId = 11,
					RoleId = 2,
					Name = "Can",
					Surname = "Öztürk",
					Email = "can.ozturk@company.com",
					PasswordHash = "TempHash_User123",
					UserCreatedAt = new DateTime(2024, 1, 13, 10, 30, 0)
				},
				new User
				{
					UserId = 12,
					RoleId = 2,
					Name = "Selin",
					Surname = "Kurt",
					Email = "selin.kurt@company.com",
					PasswordHash = "TempHash_User123",
					UserCreatedAt = new DateTime(2024, 1, 14, 12, 0, 0)
				}
			);
		}
	}
}
