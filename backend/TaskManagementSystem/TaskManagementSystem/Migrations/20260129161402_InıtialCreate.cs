using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InıtialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId        = table.Column<int>     (type: "int"          , nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    RoleId        = table.Column<int>     (type: "int"          , nullable: false),
                    Name          = table.Column<string>  (type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname       = table.Column<string>  (type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email         = table.Column<string>  (type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash  = table.Column<string>  (type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserCreatedAt = table.Column<DateTime>(type: "datetime2"    , nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Title            = table.Column<string>  (type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description      = table.Column<string>  (type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status           = table.Column<string>  (type: "nvarchar(10)" , maxLength: 10, nullable: false, defaultValue: "ToDo"),
                    AssignedUserId   = table.Column<int>     (type: "int"          , nullable: false),
                    CreatedByAdminId = table.Column<int>     (type: "int"          , nullable: false),
                    DueDate          = table.Column<DateTime>(type: "datetime2"    , nullable: true),
                    TaskCreatedAt    = table.Column<DateTime>(type: "datetime2"    , nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_AssignedUserId",
                        column: x => x.AssignedUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_CreatedByAdminId",
                        column: x => x.CreatedByAdminId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name", "PasswordHash", "RoleId", "Surname", "UserCreatedAt" },
                values: new object[,]
                {
                    { 1, "admin@company.com", "Saliha", "admin123", 0, "Ünersoy", new DateTime(2026, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "admin2@company.com", "Admin", "admin123", 0, "Şen", new DateTime(2026, 1, 2, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "ahmet.yilmaz@company.com", "Ahmet", "ahmet123", 1, "Yılmaz", new DateTime(2026, 1, 5, 9, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "ayse.kaya@company.com", "Ayşe", "ayse123", 1, "Kaya", new DateTime(2026, 1, 6, 10, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "mehmet.demir@company.com", "Mehmet", "$2a$11$cD3eF4gH5iJ6kL7mN8oP9qR0sT1uV2wX3yZ4aB5C", 1, "Demir", new DateTime(2026, 1, 7, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "fatma.sahin@company.com", "Fatma", "$2a$11$dE4fG5hI6jK7lM8nO9pQ0rS1tU2vW3xY4zA5bC6D", 1, "Şahin", new DateTime(2026, 1, 8, 14, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "ali.celik@company.com", "Ali", "$2a$11$eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5aB6cD7E", 1, "Çelik", new DateTime(2026, 1, 9, 8, 45, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "zeynep.aydin@company.com", "Zeynep", "$2a$11$fG6hI7jK8lM9nO0pQ1rS2tU3vW4xY5zA6bC7dE8F", 1, "Aydın", new DateTime(2026, 1, 10, 13, 20, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "mustafa.koc@company.com", "Mustafa", "$2a$11$gH7iJ8kL9mN0oP1qR2sT3uV4wX5yZ6aB7cD8eF9G", 1, "Koç", new DateTime(2026, 1, 11, 15, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "elif.arslan@company.com", "Elif", "$2a$11$hI8jK9lM0nO1pQ2rS3tU4vW5xY6zA7bC8dE9fG0H", 1, "Arslan", new DateTime(2026, 1, 12, 9, 50, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "can.ozturk@company.com", "Can", "$2a$11$iJ9kL0mN1oP2qR3sT4uV5wX6yZ7aB8cD9eF0gH1I", 1, "Öztürk", new DateTime(2026, 1, 13, 10, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "selin.kurt@company.com", "Selin", "$2a$11$jK0lM1nO2pQ3rS4tU5vW6xY7zA8bC9dE0fG1hI2J", 1, "Kurt", new DateTime(2026, 1, 14, 12, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "AssignedUserId", "CreatedByAdminId", "Description", "DueDate", "Status", "TaskCreatedAt", "Title" },
                values: new object[,]
                {
                    { 1, 3, 1, "Yeni proje için teknik dokümantasyon hazırlanacak", new DateTime(2026, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "InProgress", new DateTime(2026, 1, 20, 10, 0, 0, 0, DateTimeKind.Unspecified), "Proje Dokümantasyonu Hazırlama" },
                    { 2, 4, 1, "MySQL veritabanı şeması oluşturulacak", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Done", new DateTime(2026, 1, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), "Veritabanı Tasarımı" },
                    { 3, 5, 2, "RESTful API endpoint'leri geliştirilecek", new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ToDo", new DateTime(2026, 1, 22, 11, 30, 0, 0, DateTimeKind.Unspecified), "API Endpoint Geliştirme" },
                    { 4, 6, 1, "Next.js ile backend entegrasyonu yapılacak", new DateTime(2026, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "InProgress", new DateTime(2026, 1, 25, 13, 0, 0, 0, DateTimeKind.Unspecified), "Frontend Entegrasyonu" },
                    { 5, 7, 2, "Unit ve integration testler yazılacak", new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ToDo", new DateTime(2026, 1, 26, 15, 0, 0, 0, DateTimeKind.Unspecified), "Test Senaryoları Yazma" },
                    { 6, 8, 1, "Dashboard için grafik bileşenleri eklenecek", new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "InProgress", new DateTime(2026, 1, 27, 10, 30, 0, 0, DateTimeKind.Unspecified), "Chart.js Entegrasyonu" },
                    { 7, 3, 2, "Kimlik doğrulama sistemi kurulacak", new DateTime(2026, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Done", new DateTime(2026, 1, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), "JWT Authentication" },
                    { 8, 9, 1, "OpenXML ile rapor export fonksiyonu eklenecek", new DateTime(2026, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ToDo", new DateTime(2026, 1, 28, 14, 0, 0, 0, DateTimeKind.Unspecified), "Excel Export Özelliği" },
                    { 9, 10, 2, "Kullanıcılara email gönderme servisi", new DateTime(2026, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "InProgress", new DateTime(2026, 1, 29, 11, 0, 0, 0, DateTimeKind.Unspecified), "Email Bildirimi Sistemi" },
                    { 10, 11, 1, "Kullanıcı bilgilerini görüntüleme ve düzenleme", new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Done", new DateTime(2026, 1, 16, 10, 0, 0, 0, DateTimeKind.Unspecified), "Kullanıcı Profil Sayfası" },
                    { 11, 12, 2, "Admin ve kullanıcı rollerini yönetme", new DateTime(2026, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "ToDo", new DateTime(2026, 1, 30, 9, 0, 0, 0, DateTimeKind.Unspecified), "Rol Yönetim Sistemi" },
                    { 12, 3, 1, "Ana sayfa dashboard bileşenleri", new DateTime(2026, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "InProgress", new DateTime(2026, 1, 21, 14, 30, 0, 0, DateTimeKind.Unspecified), "Dashboard Tasarımı" },
                    { 13, 4, 2, "Veritabanı sorgu optimizasyonları", new DateTime(2026, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "ToDo", new DateTime(2026, 1, 31, 10, 15, 0, 0, DateTimeKind.Unspecified), "Performans Optimizasyonu" },
                    { 14, 5, 1, "Penetrasyon testleri ve güvenlik analizi", new DateTime(2026, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "InProgress", new DateTime(2026, 1, 23, 13, 45, 0, 0, DateTimeKind.Unspecified), "Güvenlik Testleri" },
                    { 15, 6, 2, "Mobil cihazlar için responsive tasarım", new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Done", new DateTime(2026, 1, 17, 11, 20, 0, 0, DateTimeKind.Unspecified), "Mobil Responsive Tasarım" },
                    { 16, 7, 1, "Gelişmiş arama ve filtreleme özellikleri", new DateTime(2026, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "ToDo", new DateTime(2026, 2, 1, 9, 30, 0, 0, DateTimeKind.Unspecified), "Arama ve Filtreleme" },
                    { 17, 8, 2, "Kullanıcı bildirim ayarları", new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "InProgress", new DateTime(2026, 2, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), "Bildirim Tercihleri" },
                    { 18, 9, 1, "Otomatik veri yedekleme mekanizması", new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ToDo", new DateTime(2026, 2, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), "Veri Yedekleme Sistemi" },
                    { 19, 10, 2, "Swagger/OpenAPI dokümantasyonu", new DateTime(2026, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Done", new DateTime(2026, 1, 19, 15, 30, 0, 0, DateTimeKind.Unspecified), "API Dokümantasyonu" },
                    { 20, 11, 1, "Merkezi loglama ve hata takibi", new DateTime(2026, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "InProgress", new DateTime(2026, 2, 4, 11, 0, 0, 0, DateTimeKind.Unspecified), "Loglama Sistemi" },
                    { 21, 12, 2, "2FA güvenlik katmanı ekleme", new DateTime(2026, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ToDo", new DateTime(2026, 2, 5, 9, 45, 0, 0, DateTimeKind.Unspecified), "İki Faktörlü Doğrulama" },
                    { 22, 3, 1, "API rate limiting implementasyonu", new DateTime(2026, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "InProgress", new DateTime(2026, 2, 6, 13, 15, 0, 0, DateTimeKind.Unspecified), "Rate Limiting" },
                    { 23, 4, 2, "i18n entegrasyonu (TR, EN)", new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ToDo", new DateTime(2026, 2, 7, 10, 30, 0, 0, DateTimeKind.Unspecified), "Çoklu Dil Desteği" },
                    { 24, 5, 1, "Dosya upload ve depolama sistemi", new DateTime(2026, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Done", new DateTime(2026, 1, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), "Dosya Yükleme Servisi" },
                    { 25, 6, 2, "Redis ile caching implementasyonu", new DateTime(2026, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "InProgress", new DateTime(2026, 2, 8, 14, 45, 0, 0, DateTimeKind.Unspecified), "Cache Mekanizması" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedUserId",
                table: "Tasks",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatedByAdminId",
                table: "Tasks",
                column: "CreatedByAdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
