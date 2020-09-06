using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YoloBlogger.Models
{
    public class UserProfileDetails
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserDetailss")]
        public int UserId { get; set; }
        [Required]
        [RegularExpression(@"^[\w\s]+$", ErrorMessage = "Enter Valid Name")]
        [Display(Name ="UserName (Mandatory)")]
        public string UserName { get; set; }
        public string Bio { get; set; }


        public UserDetails UserDetailss { get; set; }
    }
}