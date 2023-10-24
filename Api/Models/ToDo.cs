using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class ToDo
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "Informação Obrigatória")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public string Task { get; set; } = String.Empty;
        public bool IsCompleted { get; set; }= false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public ToDo()
        {
            this.Id = 0;
            this.Task = "";
            this.IsCompleted = false;
            this.CreatedAt = DateTime.Now;
        }

        public ToDo(string task, Category? category)
        {
            this.Task = task;
            this.Category = category;
        }

        public ToDo(int id, string task, bool isCompleted, DateTime createdAt)
        {
            this.Id = id;
            this.Task = task;
            this.IsCompleted = isCompleted;
            this.CreatedAt = createdAt;
        }
    }
}