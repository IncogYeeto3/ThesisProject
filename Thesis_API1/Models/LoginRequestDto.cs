using System.ComponentModel.DataAnnotations;

namespace Thesis_API1.Models
{
    public class LoginRequestDto
    {
        [Required]        
        public string Username { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
