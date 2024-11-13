using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Identity_sql_server.Models
{
    [Index(nameof(email), IsUnique = true)]
    [Index(nameof(username), IsUnique = true)]
    [Index(nameof(realname), IsUnique = true)]
    public class Customers
    {
        [Key]
        public int customers_id { get; set; }

        [Required(ErrorMessage = "username is required")]
        [MaxLength(16, ErrorMessage = "Max 16 characters allowed")]
        public string username { get; set; }

        [Required(ErrorMessage = "password is required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters allowed")]
        public string password { get; set; }

        [Required(ErrorMessage = "realname is required")]
        public string realname { get; set; }

        [Required(ErrorMessage = "email is required")]
        [MaxLength(36, ErrorMessage = "Max 36 characters allowed")]
        public string email { get; set; }

        [Required(ErrorMessage = "phone is required")]
        public string phone { get; set; }
    }
}
