namespace TaskManagementSystem.Models
{
	public class User
	{
		public int	    UserId		  { get; set; }
		public int      RoleId	      { get; set; }
		public string   Name		  { get; set; }
		public string   Surname		  { get; set; }
		public string   Email         { get; set; }
		public string   PasswordHash  { get; set; }
		public DateTime UserCreatedAt { get; set; }
	}
}
