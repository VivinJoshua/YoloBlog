using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using YoloBlogger.Models;
namespace YoloBlogger.ViewModels
{
    public class RegisterViewModel
    {
        public UserDetails UserDetails { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}