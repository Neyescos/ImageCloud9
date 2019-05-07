using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageCloud.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public int Id { get; set; }
        public string Password { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsBanned { get; set; }
        public string Email { get; set; }
    }
}