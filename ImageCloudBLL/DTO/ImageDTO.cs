using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ImageCloudBLL.DTO
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public DateTime ImageDate { get; set; }
        public int UserId { get; set; }
        public string Picture { get; set; }
        
    }
}
