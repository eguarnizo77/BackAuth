
using System.ComponentModel.DataAnnotations;

namespace BackAuth.Model.Request
{
    public class UserEmailRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
