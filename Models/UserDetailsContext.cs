using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace YoloBlogger.Models
{
        public class UserDetailsContext : DbContext
        {
            public UserDetailsContext() : base("name=ConnectionString") { }


            public DbSet<UserDetails> UserDetails { get; set; }
            public DbSet<UserProfileDetails> UserProfileDetails { get; set; }
            public DbSet<Posts> Posts { get; set; }
            public DbSet<LikesDetails> LikesDetails { get; set; }
            public DbSet<Comments> Comments { get; set; }


        }
}