using AutoMapper;
using ImageCloudBLL.DTO;
using ImageCloudBLL.Infrastructure;
using ImageCloudBLL.Interfaces;
using ImageCloudDAL.Entities;
using ImageCloudDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ImageCloudBLL.Services
{
   public class ImageService : IService<ImageDTO>
    {
        public IUnitOfWork Database { get; set; }

        public ImageService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<ImageDTO> GetElements()
        {
            List<ImageDTO> images = new List<ImageDTO>();
            foreach (var el in Database.Images.GetAll())
            {
                images.Add(new ImageDTO {Id = el.Id,ImageDate = el.ImageDate,ImageName = el.ImageName,UserId = el.UserId,Picture = el.FileAdress });
            }
            return images;
        }
      

        



        public void Make(ImageDTO Element)
        {
            Image pic = new Image { Id = Element.Id, ImageName = Element.ImageName, ImageDate = Element.ImageDate, UserId = Element.UserId, FileAdress = Element.Picture };
            Database.Images.Create(pic);
            Database.Save();
        }

        public ImageDTO GetElement(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id изображения", "");
            var image = Database.Images.Get(id.Value);
            if (image == null)
                throw new ValidationException("Изображение не найдено", "");
            return new ImageDTO { ImageName = image.ImageName, ImageDate = image.ImageDate, Id = image.Id, UserId = image.UserId, Picture = image.FileAdress};
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        public void Change(ImageDTO Element)
        {
            Image pic = new Image { Id = Element.Id, ImageName = Element.ImageName, ImageDate = Element.ImageDate, UserId = Element.UserId, FileAdress = Element.Picture };
            Database.Images.Update(pic);
            Database.Save();
        }


        public void Delete(ImageDTO Element)
        {
            Database.Images.Delete(Element.Id);
            Database.Save();
        }

    }
}
