using System.ComponentModel.DataAnnotations;

namespace Api.Data.Dto.AuthDto
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Nome precisar conter 100 caracteres")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Email precisar conter 100 caracteres")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Senha precisar conter 100 caracteres")]
        public string Password { get; set; }

        public UserRegisterDto()
        {
            Name = "";
            Email = "";
            Password = "";
        }

        public UserRegisterDto(int id, string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
