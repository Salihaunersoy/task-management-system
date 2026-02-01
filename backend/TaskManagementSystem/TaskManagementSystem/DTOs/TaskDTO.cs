namespace TaskManagementSystem.DTOs
{
	public class TaskDTO
	{
		public int       TaskId           { get; set; }
		public string    Title			  { get; set; }    
		public string?   Description	  { get; set; }       
		public string    Status		      { get; set; }              
		public int       AssignedUserId   { get; set; }
		public int       CreatedByAdminId { get; set; }
		public DateTime? DueDate		  { get; set; }
		public string?   Priority		  { get; set; }
	}
}
