using ImageCloud.Variables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ImageCloud.Models
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public DateTime ImageDate { get; set; }
        public int UserId { get; set; }
        public string Picture { get; set; }
    }
}