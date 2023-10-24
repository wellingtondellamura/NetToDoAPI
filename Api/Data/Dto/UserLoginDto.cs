using System.ComponentModel.DataAnnotations;

namespace Api.Data.Dto
{
    public class UserLoginDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Email precisar conter 100 caracteres")]
        public String Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Senha precisar conter 100 caracteres")]
        public String Password { get; set; }

        public UserLoginDto()
        {
            this.Email = "";
            this.Password = "";
        }

        public UserLoginDto(Int32 id, String name, String email, String password)
        {
            this.Email = email;
            this.Password = password;
        }
    }
}
