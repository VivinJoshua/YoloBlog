using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YoloBlogger.Models
{
    public class Comments
    {
        [Key]
        public int CommentId { get; set; }

        [ForeignKey("Postss")]
        public int PostId { get; set; }

        [ForeignKey("UserDetailss")]
        public int UserId { get; set; }
        
        [Required]
        public string Comment { get; set; }

        public Posts Postss { get; set; }
        public UserDetails UserDetailss { get; set; }
    }
}