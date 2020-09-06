using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YoloBlogger.Models
{
    public class UserDetails
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+[@][a-z]+[.][a-z]+$", ErrorMessage = "Enter Proper Email")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string UserName { get; set; }
        
        
    }
}