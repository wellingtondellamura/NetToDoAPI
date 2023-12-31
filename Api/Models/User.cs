using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class User
    {
        [Key]
        public Int32 Id { get; set; }
        [Required]
        [StringLength(100)]
        public String Name { get; set; }

        [Required]
        [StringLength(100)]
        public String Email { get; set; }

        public String Role { get; set; } = "User";

        [Required]
        [StringLength(100)]
        public String Password { get; set; }

        public virtual ICollection<ToDo> ToDos { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public User()
        {
            this.Id = 0;
            this.Name = "";
            this.Email = "";
            this.Password = "";
            this.Role = "User";
        }

        public User(Int32 id, String name, String email, String password)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Role = "User";
        }
    }
}