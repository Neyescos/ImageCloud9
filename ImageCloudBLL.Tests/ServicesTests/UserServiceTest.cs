using ImageCloudDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ImageCloudBLL.Services;
using ImageCloudDAL.Entities;
using ImageCloudBLL.DTO;
using ImageCloudDAL.EF;



namespace ImageCloudBLL.Tests.ServicesTests
{
    [TestFixture]
   public class UserServiceTest
    {
        public IUnitOfWork work = new EFUnitOfWork();

        [Test]
        public void Make()
        {
            UserService service = new UserService(work);
            UserDTO user = new UserDTO { Id =101, Email = "rabeshko2000@gmail.com", IsBanned = false, IsEmailVerified = true, Password ="12345678", UserName = "Lox", UserRole = "Admin" };
            service.Make(user);
            work.Save();
            Assert.NotNull(service.GetElement(user.Id));
        }
        //[Test]
        public void GetElement()
        {
            Func<User, bool> func = x => x.Email == "sobakoed@gmail.com";
            UserService service = new UserService(work);
            UserDTO user = new UserDTO { Email = "sobakoed@gmail.com", IsBanned = false, IsEmailVerified = false, Password = "12345678", UserName = "Lox", UserRole = "Tvar" };
            service.Make(user);
           User u =  work.Users.Find(func).ElementAt(0);
            user.Id = u.Id;
            Assert.That(user ==service.GetElement(u.Id));
        }
        //[Test]
        public void GetElements()
        {
            UserService service = new UserService(work);
            Assert.IsEmpty(service.GetElements());
        }
        //[Test]
        public void Delete()
        {
            UserService service = new UserService(work);
            UserDTO user = new UserDTO { Id = 1, Email = "tipes@gmail.com", IsBanned = false, IsEmailVerified = false, Password = "12345678", UserName = "Lox", UserRole = "Tvar" };
            service.Delete(user);
            work.Save();
            Assert.IsNull(service.GetElement(1));
        }
        public void Change()
        {

        }
        [Test]
        public void Clear()
        {
            foreach (var el in work.Users.GetAll())
            {
                work.Users.Delete(el.Id);

            }
            work.Save();
        }
       
    }
}
