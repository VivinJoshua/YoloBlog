using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YoloBlogger.Models
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }

        [ForeignKey("UserDetailss")]
        public int UserId { get; set; }
        [Required]
        public string Post { get; set; }
        public int Like { get; set; }

        public string UserName { get; set; }
        public DateTime PostDateTime { get; set; }
        public UserDetails UserDetailss { get; set; }
        public UserProfileDetails UserProfileDetailss { get; set; }

        public ICollection<Comments> Comments { get; set; }
    }
}