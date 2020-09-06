using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YoloBlogger.Models
{
    public class LikesDetails
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Postss")]
        public int PostId { get; set; }


        [ForeignKey("UserDetailss")]
        public int UserId { get; set; }

        public UserDetails UserDetailss { get; set; }
        public Posts Postss { get; set; }
    }
}