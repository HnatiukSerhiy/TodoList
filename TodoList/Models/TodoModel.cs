using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter description")] public string Description { get; set; } = String.Empty;
        public bool IsDone { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? DoneTime { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

}
