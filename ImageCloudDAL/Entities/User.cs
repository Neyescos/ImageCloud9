using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCloudDAL.Entities
{
   public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool IsBanned { get; set; }
        public string UserRole { get; set; }
    }
}
