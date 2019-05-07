using ImageCloudDAL.EF;
using ImageCloudDAL.Entities;
using ImageCloudDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCloudDAL.Repositories
{
    public class ImageRepository:IRepository<Image>
    {
        private ImageContext db;

        public ImageRepository(ImageContext context)
        {
            this.db = context;
        }

        public IEnumerable<Image> GetAll()
        {
            return db.Images;
        }

        public Image Get(int id)
        {
            return db.Images.Find(id);
        }

        public void Create(Image element)
        {
            db.Images.Add(element);
        }

        public void Update(Image element)
        {
            db.Entry(element).State = EntityState.Modified;
        }

        public IEnumerable<Image> Find(Func<Image, Boolean> predicate)
        {
            return db.Images.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Image element = db.Images.Find(id);
            if (element != null)
                db.Images.Remove(element);
        }
    }
}
