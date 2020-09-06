using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YoloBlogger.Models;

namespace YoloBlogger.ViewModels
{
    public class ProfilepageViewModel
    {
        public UserProfileDetails UserProfileDetails { get; set; }
        public List<Posts> Posts { get; set; }

        public List<Comments> Comments { get; set; }
        public IEnumerable<LikesDetails> LikesDetails { get; set; }

        [Required]
        public string UserComment { get; set; }
    }
}