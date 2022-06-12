using System.ComponentModel.DataAnnotations;

namespace BackAuth.Model.Request
{
    public class AuthRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
