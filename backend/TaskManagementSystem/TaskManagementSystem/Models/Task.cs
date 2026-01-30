namespace TaskManagementSystem.Models
{
	public class Task
	{
		public int       TaskId			  { get; set; }
		public string    Title			  { get; set; }  
		public string?   Description      { get; set; } 
		public string    Status			  { get; set; }  
		public int       AssignedUserId   { get; set; }    
		public int       CreatedByAdminId { get; set; }   
		public DateTime? DueDate		  { get; set; }     
		public DateTime  TaskCreatedAt	  { get; set; }

		//public User     AssignedUser     { get; set; }
		//public User     CreatedByAdmin   { get; set; }
	}
}
