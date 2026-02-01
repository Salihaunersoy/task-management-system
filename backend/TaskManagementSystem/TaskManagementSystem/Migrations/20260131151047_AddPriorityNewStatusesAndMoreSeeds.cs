using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddPriorityNewStatusesAndMoreSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Tasks",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "ToDo",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldDefaultValue: "ToDo");

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Tasks",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "Medium");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 1,
                column: "Priority",
                value: "High");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 2,
                column: "Priority",
                value: "Critical");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 3,
                column: "Priority",
                value: "High");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 4,
                columns: new[] { "Priority", "Status" },
                values: new object[] { "High", "Testing" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 5,
                column: "Priority",
                value: "Medium");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 6,
                columns: new[] { "Priority", "Status" },
                values: new object[] { "Medium", "Testing" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 7,
                column: "Priority",
                value: "Critical");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 8,
                columns: new[] { "Priority", "Status" },
                values: new object[] { "Low", "OnHold" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 9,
                columns: new[] { "Priority", "Status" },
                values: new object[] { "Medium", "OnHold" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 10,
                column: "Priority",
                value: "Medium");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 11,
                column: "Priority",
                value: "High");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 12,
                column: "Priority",
                value: "Medium");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 13,
                column: "Priority",
                value: "Low");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 14,
                columns: new[] { "Priority", "Status" },
                values: new object[] { "Critical", "Testing" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 15,
                column: "Priority",
                value: "Medium");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 16,
                column: "Priority",
                value: "Medium");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 17,
                column: "Priority",
                value: "Low");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 18,
                columns: new[] { "Priority", "Status" },
                values: new object[] { "High", "OnHold" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 19,
                column: "Priority",
                value: "Low");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 20,
                column: "Priority",
                value: "Medium");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 21,
                column: "Priority",
                value: "Critical");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 22,
                columns: new[] { "Priority", "Status" },
                values: new object[] { "High", "Testing" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 23,
                column: "Priority",
                value: "Low");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 24,
                column: "Priority",
                value: "Medium");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 25,
                column: "Priority",
                value: "High");

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "AssignedUserId", "CreatedByAdminId", "Description", "DueDate", "Priority", "Status", "TaskCreatedAt", "Title" },
                values: new object[,]
                {
                    { 26, 7, 1, "Üçüncü parti servisler için webhook desteği", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "High", "ToDo", new DateTime(2026, 2, 9, 9, 0, 0, 0, DateTimeKind.Unspecified), "Webhook Entegrasyonu" },
                    { 27, 8, 2, "Kullanıcı arayüzüne karanlık tema eklenmesi", new DateTime(2026, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Low", "InProgress", new DateTime(2026, 2, 10, 10, 30, 0, 0, DateTimeKind.Unspecified), "Dark Mode Desteği" },
                    { 28, 9, 1, "Görev raporlarını PDF olarak dışa aktarma", new DateTime(2026, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Testing", new DateTime(2026, 2, 11, 11, 15, 0, 0, DateTimeKind.Unspecified), "PDF Rapor Oluşturma" },
                    { 29, 10, 2, "Kullanıcı aktivitelerinin kayıt altına alınması", new DateTime(2026, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "OnHold", new DateTime(2026, 2, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), "Aktivite Logları" },
                    { 30, 11, 1, "Birden fazla görevi aynı anda kullanıcıya atama", new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "Done", new DateTime(2026, 2, 13, 9, 45, 0, 0, DateTimeKind.Unspecified), "Toplu Görev Atama" },
                    { 31, 12, 2, "Görevleri takvim üzerinde görselleştirme", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "ToDo", new DateTime(2026, 2, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), "Takvim Görünümü" },
                    { 32, 3, 1, "Sürükle bırak ile görev durumu değiştirme", new DateTime(2026, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "High", "InProgress", new DateTime(2026, 2, 15, 13, 30, 0, 0, DateTimeKind.Unspecified), "Drag & Drop Kanban Board" },
                    { 33, 4, 2, "Yaklaşan son tarihler için otomatik bildirim", new DateTime(2026, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "High", "Testing", new DateTime(2026, 2, 16, 8, 30, 0, 0, DateTimeKind.Unspecified), "Otomatik Hatırlatıcılar" },
                    { 34, 5, 1, "Profil fotoğrafı yükleme ve gösterme", new DateTime(2026, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Low", "Done", new DateTime(2026, 2, 17, 15, 0, 0, 0, DateTimeKind.Unspecified), "Kullanıcı Avatarları" },
                    { 35, 6, 2, "SignalR ile anlık bildirim sistemi", new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Critical", "OnHold", new DateTime(2026, 2, 18, 11, 0, 0, 0, DateTimeKind.Unspecified), "Gerçek Zamanlı Bildirimler" },
                    { 36, 7, 1, "Görevlere yorum ekleme ve tartışma özelliği", new DateTime(2026, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medium", "InProgress", new DateTime(2026, 2, 19, 9, 15, 0, 0, DateTimeKind.Unspecified), "Görev Yorumları" },
                    { 37, 8, 2, "Görevlere renk kodlu etiket atama sistemi", new DateTime(2026, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Low", "Testing", new DateTime(2026, 2, 20, 14, 30, 0, 0, DateTimeKind.Unspecified), "Görev Etiketleri" },
                    { 38, 9, 1, "Görev verilerini JSON/CSV olarak dışa aktarma", new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Low", "ToDo", new DateTime(2026, 2, 21, 10, 45, 0, 0, DateTimeKind.Unspecified), "Dışa Aktarma API" },
                    { 39, 10, 2, "Görevler arası bağımlılık tanımlama", new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Critical", "ToDo", new DateTime(2026, 2, 22, 13, 0, 0, 0, DateTimeKind.Unspecified), "Görev Bağımlılıkları" },
                    { 40, 11, 1, "Kullanıcı performans metrikleri ve raporları", new DateTime(2026, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "High", "InProgress", new DateTime(2026, 2, 23, 11, 30, 0, 0, DateTimeKind.Unspecified), "Performans Dashboard" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "PasswordHash",
                value: "mehmet123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6,
                column: "PasswordHash",
                value: "fatma123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                column: "PasswordHash",
                value: "ali123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                column: "PasswordHash",
                value: "zeynep123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                column: "PasswordHash",
                value: "mustafa123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10,
                column: "PasswordHash",
                value: "elif123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 11,
                column: "PasswordHash",
                value: "can123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 12,
                column: "PasswordHash",
                value: "selin123");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 40);

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Tasks");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Tasks",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "ToDo",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldDefaultValue: "ToDo");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 4,
                column: "Status",
                value: "InProgress");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 6,
                column: "Status",
                value: "InProgress");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 8,
                column: "Status",
                value: "ToDo");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 9,
                column: "Status",
                value: "InProgress");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 14,
                column: "Status",
                value: "InProgress");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 18,
                column: "Status",
                value: "ToDo");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 22,
                column: "Status",
                value: "InProgress");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                column: "PasswordHash",
                value: "$2a$11$cD3eF4gH5iJ6kL7mN8oP9qR0sT1uV2wX3yZ4aB5C");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 6,
                column: "PasswordHash",
                value: "$2a$11$dE4fG5hI6jK7lM8nO9pQ0rS1tU2vW3xY4zA5bC6D");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 7,
                column: "PasswordHash",
                value: "$2a$11$eF5gH6iJ7kL8mN9oP0qR1sT2uV3wX4yZ5aB6cD7E");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 8,
                column: "PasswordHash",
                value: "$2a$11$fG6hI7jK8lM9nO0pQ1rS2tU3vW4xY5zA6bC7dE8F");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 9,
                column: "PasswordHash",
                value: "$2a$11$gH7iJ8kL9mN0oP1qR2sT3uV4wX5yZ6aB7cD8eF9G");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 10,
                column: "PasswordHash",
                value: "$2a$11$hI8jK9lM0nO1pQ2rS3tU4vW5xY6zA7bC8dE9fG0H");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 11,
                column: "PasswordHash",
                value: "$2a$11$iJ9kL0mN1oP2qR3sT4uV5wX6yZ7aB8cD9eF0gH1I");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 12,
                column: "PasswordHash",
                value: "$2a$11$jK0lM1nO2pQ3rS4tU5vW6xY7zA8bC9dE0fG1hI2J");
        }
    }
}
