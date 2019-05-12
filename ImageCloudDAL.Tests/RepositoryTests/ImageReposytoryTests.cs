using ImageCloudDAL.EF;
using ImageCloudDAL.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCloudDAL.Tests.RepositoryTests
{
    [TestFixture]
    public class ImageReposytoryTests
    {
        public EFUnitOfWork work =new EFUnitOfWork();
        [Test]
        public void CreateImage()
        {
            work.Images.Create(new Image {
                Id = 1,
                ImageName = "NewImage",
                FileAdress = null,
                ImageDate = new DateTime(),
                UserId = 1,
            });
            Assert.IsNotNull(work.Images.Get(1));
        }
        [Test]
        public void UpdateImage()
        {
            Image image=  work.Images.Get(1);
            work.Images.Update(new Image
            {
                Id = 1,
                ImageName = "I34",
                FileAdress = null,
                ImageDate = new DateTime(),
                UserId = 1,
            });
            Assert.AreNotEqual(image, work.Images.Get(1));
        }
        [Test]
        public void DeleteImage()
        {
            work.Images.Delete(16);
            work.Save();
            Assert.IsNull(work.Images.Get(16));
        }
    }
}
