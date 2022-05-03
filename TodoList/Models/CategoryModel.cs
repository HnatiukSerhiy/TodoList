using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter category name")]
        public string CategoryName { get; set; } = string.Empty;
    }
}
