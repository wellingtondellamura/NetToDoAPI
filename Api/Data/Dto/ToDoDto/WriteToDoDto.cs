using Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.Data.Dto.ToDoDto
{
    public class WriteToDoDto
    {
        
        [Required(ErrorMessage = "Informação Obrigatória")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Task { get; set; } = String.Empty;

        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public int CategoryId { get; set; }

        public WriteToDoDto()
        {
            this.Task = "";
        }

        public WriteToDoDto(string task, int category)
        {
            this.Task = task;
            this.CategoryId = category;
        }

        public WriteToDoDto(string task, bool isCompleted, DateTime createdAt)
        {
            this.Task = task;
            this.IsCompleted = isCompleted;
            this.CreatedAt = createdAt;
        }
        
    }
}
