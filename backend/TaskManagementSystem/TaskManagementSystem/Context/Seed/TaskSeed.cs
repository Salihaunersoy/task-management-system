using Microsoft.EntityFrameworkCore;
using Task = TaskManagementSystem.Models.Task;

namespace TaskManagementSystem.Context.Seed
{
	public static class TaskSeed
	{
		public static void SeedTasks(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Task>().HasData(
				new Task
				{
					TaskId = 1,
					Title = "Proje Dokümantasyonu Hazırlama",
					Description = "Yeni proje için teknik dokümantasyon hazırlanacak",
					Status = "InProgress",
					AssignedUserId = 3,
					CreatedByAdminId = 1,
					DueDate = new DateTime(2026, 2, 15),
					TaskCreatedAt = new DateTime(2026, 1, 20, 10, 0, 0)
				},
				new Task
				{
					TaskId = 2,
					Title = "Veritabanı Tasarımı",
					Description = "MySQL veritabanı şeması oluşturulacak",
					Status = "Done",
					AssignedUserId = 4,
					CreatedByAdminId = 1,
					DueDate = new DateTime(2026, 2, 1),
					TaskCreatedAt = new DateTime(2026, 1, 18, 9, 0, 0)
				},
				new Task
				{
					TaskId = 3,
					Title = "API Endpoint Geliştirme",
					Description = "RESTful API endpoint'leri geliştirilecek",
					Status = "ToDo",
					AssignedUserId = 5,
					CreatedByAdminId = 2,
					DueDate = new DateTime(2026, 3, 1),
					TaskCreatedAt = new DateTime(2026, 1, 22, 11, 30, 0)
				},
				new Task
				{
					TaskId = 4,
					Title = "Frontend Entegrasyonu",
					Description = "Next.js ile backend entegrasyonu yapılacak",
					Status = "InProgress",
					AssignedUserId = 6,
					CreatedByAdminId = 1,
					DueDate = new DateTime(2026, 3, 15),
					TaskCreatedAt = new DateTime(2026, 1, 25, 13, 0, 0)
				},
				new Task
				{
					TaskId = 5,
					Title = "Test Senaryoları Yazma",
					Description = "Unit ve integration testler yazılacak",
					Status = "ToDo",
					AssignedUserId = 7,
					CreatedByAdminId = 2,
					DueDate = new DateTime(2026, 3, 20),
					TaskCreatedAt = new DateTime(2026, 1, 26, 15, 0, 0)
				},
				new Task
				{
					TaskId = 6,
					Title = "Chart.js Entegrasyonu",
					Description = "Dashboard için grafik bileşenleri eklenecek",
					Status = "InProgress",
					AssignedUserId = 8,
					CreatedByAdminId = 1,
					DueDate = new DateTime(2026, 2, 28),
					TaskCreatedAt = new DateTime(2026, 1, 27, 10, 30, 0)
				},
				new Task
				{
					TaskId = 7,
					Title = "JWT Authentication",
					Description = "Kimlik doğrulama sistemi kurulacak",
					Status = "Done",
					AssignedUserId = 3,
					CreatedByAdminId = 2,
					DueDate = new DateTime(2026, 1, 31),
					TaskCreatedAt = new DateTime(2026, 1, 15, 9, 0, 0)
				},
				new Task
				{
					TaskId = 8,
					Title = "Excel Export Özelliği",
					Description = "OpenXML ile rapor export fonksiyonu eklenecek",
					Status = "ToDo",
					AssignedUserId = 9,
					CreatedByAdminId = 1,
					DueDate = new DateTime(2026, 3, 10),
					TaskCreatedAt = new DateTime(2026, 1, 28, 14, 0, 0)
				},
				new Task
				{
					TaskId = 9,
					Title = "Email Bildirimi Sistemi",
					Description = "Kullanıcılara email gönderme servisi",
					Status = "InProgress",
					AssignedUserId = 10,
					CreatedByAdminId = 2,
					DueDate = new DateTime(2026, 3, 5),
					TaskCreatedAt = new DateTime(2026, 1, 29, 11, 0, 0)
				},
				new Task
				{
					TaskId = 10,
					Title = "Kullanıcı Profil Sayfası",
					Description = "Kullanıcı bilgilerini görüntüleme ve düzenleme",
					Status = "Done",
					AssignedUserId = 11,
					CreatedByAdminId = 1,
					DueDate = new DateTime(2026, 2, 10),
					TaskCreatedAt = new DateTime(2026, 1, 16, 10, 0, 0)
				},
				new Task
				{
					TaskId = 11,
					Title = "Rol Yönetim Sistemi",
					Description = "Admin ve kullanıcı rollerini yönetme",
					Status = "ToDo",
					AssignedUserId = 12,
					CreatedByAdminId = 2,
					DueDate = new DateTime(2026, 3, 25),
					TaskCreatedAt = new DateTime(2026, 1, 30, 9, 0, 0)
				},
				new Task
				{
					TaskId = 12,
					Title = "Dashboard Tasarımı",
					Description = "Ana sayfa dashboard bileşenleri",
					Status = "InProgress",
					AssignedUserId = 3,
					CreatedByAdminId = 1,
					DueDate = new DateTime(2026, 2, 20),
					TaskCreatedAt = new DateTime(2026, 1, 21, 14, 30, 0)
				},
				new Task
				{
					TaskId = 13,
					Title = "Performans Optimizasyonu",
					Description = "Veritabanı sorgu optimizasyonları",
					Status = "ToDo",
					AssignedUserId = 4,
					CreatedByAdminId = 2,
					DueDate = new DateTime(2026, 3, 30),
					TaskCreatedAt = new DateTime(2026, 1, 31, 10, 15, 0)
				},
				new Task
				{
					TaskId = 14,
					Title = "Güvenlik Testleri",
					Description = "Penetrasyon testleri ve güvenlik analizi",
					Status = "InProgress",
					AssignedUserId = 5,
					CreatedByAdminId = 1,
					DueDate = new DateTime(2026, 3, 12),
					TaskCreatedAt = new DateTime(2026, 1, 23, 13, 45, 0)
				},
				new Task
				{
					TaskId = 15,
					Title = "Mobil Responsive Tasarım",
					Description = "Mobil cihazlar için responsive tasarım",
					Status = "Done",
					AssignedUserId = 6,
					CreatedByAdminId = 2,
					DueDate = new DateTime(2026, 2, 5),
					TaskCreatedAt = new DateTime(2026, 1, 17, 11, 20, 0)
				},
				new Task
				{
					TaskId = 16,
					Title = "Arama ve Filtreleme",
					Description = "Gelişmiş arama ve filtreleme özellikleri",
					Status = "ToDo",
					AssignedUserId = 7,
					CreatedByAdminId = 1,
					DueDate = new DateTime(2026, 3, 18),
					TaskCreatedAt = new DateTime(2026, 2, 1, 9, 30, 0)
				},
				new Task
				{
					TaskId = 17,
					Title = "Bildirim Tercihleri",
					Description = "Kullanıcı bildirim ayarları",
					Status = "InProgress",
					AssignedUserId = 8,
					CreatedByAdminId = 2,
					DueDate = new DateTime(2026, 3, 8),
					TaskCreatedAt = new DateTime(2026, 2, 2, 10, 0, 0)
				},
				new Task
				{
					TaskId = 18,
					Title = "Veri Yedekleme Sistemi",
					Description = "Otomatik veri yedekleme mekanizması",
					Status = "ToDo",
					AssignedUserId = 9,
					CreatedByAdminId = 1,
					DueDate = new DateTime(2026, 4, 1),
					TaskCreatedAt = new DateTime(2026, 2, 3, 14, 0, 0)
				},
				new Task
				{
					TaskId = 19,
					Title = "API Dokümantasyonu",
					Description = "Swagger/OpenAPI dokümantasyonu",
					Status = "Done",
					AssignedUserId = 10,
					CreatedByAdminId = 2,
					DueDate = new DateTime(2026, 2, 8),
					TaskCreatedAt = new DateTime(2026, 1, 19, 15, 30, 0)
				},
				new Task
				{
					TaskId = 20,
					Title = "Loglama Sistemi",
					Description = "Merkezi loglama ve hata takibi",
					Status = "InProgress",
					AssignedUserId = 11,
					CreatedByAdminId = 1,
					DueDate = new DateTime(2026, 3, 22),
					TaskCreatedAt = new DateTime(2026, 2, 4, 11, 0, 0)
				},
				new Task
				{
					TaskId = 21,
					Title = "İki Faktörlü Doğrulama",
					Description = "2FA güvenlik katmanı ekleme",
					Status = "ToDo",
					AssignedUserId = 12,
					CreatedByAdminId = 2,
					DueDate = new DateTime(2026, 4, 5),
					TaskCreatedAt = new DateTime(2026, 2, 5, 9, 45, 0)
				},
				new Task
				{
					TaskId = 22,
					Title = "Rate Limiting",
					Description = "API rate limiting implementasyonu",
					Status = "InProgress",
					AssignedUserId = 3,
					CreatedByAdminId = 1,
					DueDate = new DateTime(2026, 3, 14),
					TaskCreatedAt = new DateTime(2026, 2, 6, 13, 15, 0)
				},
				new Task
				{
					TaskId = 23,
					Title = "Çoklu Dil Desteği",
					Description = "i18n entegrasyonu (TR, EN)",
					Status = "ToDo",
					AssignedUserId = 4,
					CreatedByAdminId = 2,
					DueDate = new DateTime(2026, 4, 10),
					TaskCreatedAt = new DateTime(2026, 2, 7, 10, 30, 0)
				},
				new Task
				{
					TaskId = 24,
					Title = "Dosya Yükleme Servisi",
					Description = "Dosya upload ve depolama sistemi",
					Status = "Done",
					AssignedUserId = 5,
					CreatedByAdminId = 1,
					DueDate = new DateTime(2026, 2, 12),
					TaskCreatedAt = new DateTime(2026, 1, 24, 12, 0, 0)
				},
				new Task
				{
					TaskId = 25,
					Title = "Cache Mekanizması",
					Description = "Redis ile caching implementasyonu",
					Status = "InProgress",
					AssignedUserId = 6,
					CreatedByAdminId = 2,
					DueDate = new DateTime(2026, 3, 28),
					TaskCreatedAt = new DateTime(2026, 2, 8, 14, 45, 0)
				}
			);
		}
	}
}
