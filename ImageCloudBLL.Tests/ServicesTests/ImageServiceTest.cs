using ImageCloudBLL.DTO;
using ImageCloudBLL.Services;
using ImageCloudDAL.EF;
using ImageCloudDAL.Entities;
using ImageCloudDAL.Interfaces;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageCloudBLL.Tests.ServicesTests
{
    [TestFixture]
  public class ImageServiceTest
    {
        private const string name = @"C:\Шаражка\Графика\Dope Photo\3\Цветы.JPG";
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

           
            var fileName = Path.GetFileName(name);
            ImageService service = new ImageService(work);
            ImageDTO picture = new ImageDTO { Id = 1, ImageName = "Pupkina zalupkina", ImageDate = DateTime.Now, UserId = 1, Picture = System.Web.HttpContext.Current.Server.MapPath("~/Data/Pictures/" + fileName) };
            service.Make(picture);
            work.Save();
            Assert.AreEqual(service.GetElement(2), picture);
        }
        [Test]
        public void Make()
        {
            

            var fileName = Path.GetFileName(name);
            Func<Image, bool> func = d => d.ImageName == "Pupkina zalupkina";

            ImageService service = new ImageService(work);
            
            ImageDTO img = new ImageDTO { ImageName = "Pupkina zalupkina", ImageDate = DateTime.Now, UserId = 1, Picture = System.Web.HttpContext.Current.Server.MapPath("~/Data/Pictures/" + fileName) };
            service.Make(img);
            work.Save();
            Image im = work.Images.Find(func).ElementAt(0);
            Assert.NotNull(service.GetElement(im.Id));
        }
        [Test]
        public void Delete()
        {
            

            var fileName = Path.GetFileName(name);
            ImageService service = new ImageService(work);
            ImageDTO picture = new ImageDTO { Id = 17, ImageName = "Pupkina zalupkina", ImageDate = DateTime.Now, UserId = 1, Picture = System.Web.HttpContext.Current.Server.MapPath("~/Data/Pictures/" + fileName) };
            service.Delete(picture);
            work.Save();
            Assert.IsNull(service.GetElement(19));
        }
       // [Test]
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
