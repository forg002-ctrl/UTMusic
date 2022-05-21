using System.ComponentModel.DataAnnotations;

namespace UTMUSIC.Web.Models
{
    public class UserLogin
    {
        [Required]
        public string Credential { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}