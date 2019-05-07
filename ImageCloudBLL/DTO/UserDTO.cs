using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCloudBLL.DTO
{
    public class UserDTO
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
