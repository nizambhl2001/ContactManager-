using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMvcJwtApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
