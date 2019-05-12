using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ImageCloudDAL.Entities;

namespace ImageCloudDAL.EF
{
   public class ImageContext : DbContext
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<User>Users { get; set; }
        public ImageContext()
        {
            Database.SetInitializer<ImageContext>(new ImageDbInitializer());
        }
        public ImageContext(string connectionString)
            : base(connectionString)
        {
        }

       
    }
    public class ImageDbInitializer : DropCreateDatabaseIfModelChanges<ImageContext>
    {
        protected override void Seed(ImageContext db)
        {
        
        }
    }
}
