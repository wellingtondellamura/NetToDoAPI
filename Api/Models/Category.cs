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
        [Required]
        [StringLength(100)]
        public String Name { get; set; }

        [Required]
        [StringLength(100)]
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