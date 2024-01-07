using System.ComponentModel.DataAnnotations;

namespace Recipe.API.Models
{
    public class SignInModel
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
