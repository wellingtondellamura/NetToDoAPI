using System.ComponentModel.DataAnnotations;

namespace Api.Data.Dto.AuthDto
{
    public class UserLoginDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Email precisar conter 100 caracteres")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Senha precisar conter 100 caracteres")]
        public string Password { get; set; }

        public UserLoginDto()
        {
            Email = "";
            Password = "";
        }

        public UserLoginDto(int id, string name, string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
