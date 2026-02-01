namespace TaskManagementSystem.DTOs
{
	public class TaskAssignmentDTO
	{
		public TaskDTO Task			{ get; set; }
		public UserDTO AssignedUser { get; set; }
	}
}
