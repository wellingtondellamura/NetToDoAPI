using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Category
    {
        [Key]
        public Int32 Id { get; set; }
        [Required (ErrorMessage = "Nome da categoria � obrigat�rio")]
        [StringLength(100, ErrorMessage = "M�ximo 100 caracteres")]
        public String Name { get; set; }

        [Required (ErrorMessage = "Cor da categoria � obrigat�rio")]
        [StringLength(100, ErrorMessage = "M�ximo 100 caracteres")]
        public String Color { get; set; }

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