using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace NewsModule.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User:IdentityUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public override string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public override string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public override string PasswordHash { get; set; }


        [Required]
        public enumRoles Role { get; set; }
        

        public User(int id, string userName, string email, string password, enumRoles role) 
        {
            this.Id = id;
            this.UserName = userName;
            this.PasswordHash = password;
            this.Email = email;
            this.Role = role;
        }
        public User()
        {
            this.Id = 0;
            this.UserName = "userName";
            this.PasswordHash = "password";
            this.Email = "email";
            this.Role = enumRoles.User;
        }

    }   
}
