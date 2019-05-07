using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCloudDAL.Entities
{
   public class Image
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public DateTime ImageDate  { get; set; }
        public string FileAdress { get; set; }
        public int UserId { get; set; }
    }
}
