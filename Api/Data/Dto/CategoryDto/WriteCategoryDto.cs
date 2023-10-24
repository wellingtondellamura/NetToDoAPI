using Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.Data.Dto.CategoryDto
{
    public class WriteCategoryDto
    {
        [Required(ErrorMessage = "Nome da categoria é obrigatório")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public String Name { get; set; }

        [Required(ErrorMessage = "Cor da categoria é obrigatória")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        public String Color { get; set; }

        public WriteCategoryDto()
        {
            this.Name = "";
            this.Color = "";
        }

        public WriteCategoryDto(String name, String color)
        {
            this.Name = name;
            this.Color = color;
        }
    }
}
