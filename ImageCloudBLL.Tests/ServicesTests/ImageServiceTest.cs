using ImageCloudBLL.DTO;
using ImageCloudBLL.Services;
using ImageCloudDAL.EF;
using ImageCloudDAL.Entities;
using ImageCloudDAL.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCloudBLL.Tests.ServicesTests
{ [TestFixture]
  public class ImageServiceTest
    {
        public IUnitOfWork work = new EFUnitOfWork();
        [Test]
        public void GetElements()
        {
            ImageService service = new ImageService(work);
            
            Assert.IsEmpty(service.GetElements());

        }
        [Test]
        public void GetElement()
        {
            ImageService service = new ImageService(work);
            ImageDTO picture = new ImageDTO { Id = 1, ImageName = "Pupkina zalupkina", ImageDate = DateTime.Now, UserId = 1, Picture = System.Drawing.Image.FromFile(@"C:\Users\Андрей\Pictures\jakub-rozalski-digital-painting0.jpg") };
            service.Make(picture);
            work.Save();
            Assert.AreEqual(service.GetElement(2), picture);
        }
        [Test]
        public void Make()
        {
            Func<Image, bool> func = d => d.ImageName == "Pupkina zalupkina";

            ImageService service = new ImageService(work);
            
            ImageDTO img = new ImageDTO { ImageName = "Pupkina zalupkina", ImageDate = DateTime.Now, UserId = 1, Picture = System.Drawing.Image.FromFile(@"C:\Users\Андрей\Pictures\jakub-rozalski-digital-painting0.jpg") };
            service.Make(img);
            work.Save();
            Image im = work.Images.Find(func).ElementAt(0);
            Assert.NotNull(service.GetElement(im.Id));
        }
        [Test]
        public void Delete()
        {
            ImageService service = new ImageService(work);
            ImageDTO picture = new ImageDTO { Id = 17, ImageName = "Pupkina zalupkina", ImageDate = DateTime.Now, UserId = 1, Picture = System.Drawing.Image.FromFile(@"C:\Users\Андрей\Pictures\jakub-rozalski-digital-painting0.jpg") };
            service.Delete(picture);
            work.Save();
            Assert.IsNull(service.GetElement(19));
        }
        [Test]
        public void Clear()
        {
            foreach (var el in work.Images.GetAll())
            {
                work.Images.Delete(el.Id);

            }
            work.Save();
        }
    }
}
