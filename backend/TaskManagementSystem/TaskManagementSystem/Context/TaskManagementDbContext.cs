using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Context.Seed;
using TaskManagementSystem.Models;
using Task = TaskManagementSystem.Models.Task;

namespace TaskManagementSystem.Context
{
	public class TaskManagementDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Task> Tasks { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=.;Database=TaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>(entity =>
			{
				entity.HasKey  (u => u.UserId);

				entity.Property(u => u.RoleId)       .IsRequired();
				entity.Property(u => u.Name)         .IsRequired().HasMaxLength(100);
				entity.Property(u => u.Surname)      .IsRequired().HasMaxLength(100);
				entity.Property(u => u.Email)        .IsRequired().HasMaxLength(150);
				entity.Property(u => u.PasswordHash) .IsRequired().HasMaxLength(100);
				entity.Property(u => u.UserCreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");
			});

			modelBuilder.Entity<Task>(entity =>
			{
				entity.HasKey  (t => t.TaskId);

				entity.Property(t => t.Title)      .IsRequired().HasMaxLength(100);
				entity.Property(t => t.Status)     .IsRequired().HasMaxLength(10).HasDefaultValue("ToDo");
				entity.Property(t => t.Description).IsRequired(false).HasMaxLength(500);

				entity.Property(t => t.AssignedUserId)  .IsRequired();
				entity.Property(t => t.CreatedByAdminId).IsRequired();

				entity.Property(t => t.DueDate)      .IsRequired(false);
				entity.Property(t => t.TaskCreatedAt).IsRequired().HasDefaultValueSql("GETDATE()");

				// FK: AssignedUserId → Users.UserId
				entity.HasOne<User>()
					  .WithMany()
					  .HasForeignKey(t => t.AssignedUserId)
					  .OnDelete(DeleteBehavior.Restrict);

				// FK: CreatedByAdminId → Users.UserId
				entity.HasOne<User>()
					  .WithMany()
					  .HasForeignKey(t => t.CreatedByAdminId)
					  .OnDelete(DeleteBehavior.Restrict); 
			});
	
			UserSeed.SeedUsers(modelBuilder);
			TaskSeed.SeedTasks(modelBuilder);
		}

	}
}
