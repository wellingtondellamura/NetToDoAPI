using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    public class Category
    {
        [Key]
        public Int32 Id { get; set; }
        [Required (ErrorMessage = "Nome da categoria é obrigatório")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public String Name { get; set; }

        [Required (ErrorMessage = "Cor da categoria é obrigatória")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public String Color { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public Category()
        {
            this.Id = 0;
            this.Name = "";
            this.Color = "";
        }

        public Category(Int32 id, String name, String color)
        {
            this.Id = id;
            this.Name = name;
            this.Color = color;
        }
    }
}