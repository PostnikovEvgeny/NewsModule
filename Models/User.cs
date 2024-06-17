using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace NewsModule.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        
        public string Role { get; set; }
        

        public User(int id, string userName, string email, string password, string role) 
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
            this.Role = "user";
        }

    }   
}
