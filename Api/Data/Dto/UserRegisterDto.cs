using System.ComponentModel.DataAnnotations;

namespace Api.Data.Dto
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Nome precisar conter 100 caracteres")]
        public String Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Email precisar conter 100 caracteres")]
        public String Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Senha precisar conter 100 caracteres")]
        public String Password { get; set; }

        public UserRegisterDto()
        {
            this.Name = "";
            this.Email = "";
            this.Password = "";
        }

        public UserRegisterDto(Int32 id, String name, String email, String password)
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }   
    }
}
