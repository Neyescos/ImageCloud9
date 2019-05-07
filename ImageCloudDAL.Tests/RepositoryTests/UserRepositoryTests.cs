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
    class UserRepositoryTests
    {
        public EFUnitOfWork work = new EFUnitOfWork();
        [Test]
        public void CreateUser()
        {
            work.Users.Create(new User
            {
                Id = 1,
                Email = "babushekneobizhat@gmail.com",
                IsEmailVerified = true,
                UserName = "Lesha",
                UserPassword = "12345678",
                IsBanned = false,
            });
            Assert.IsNotNull(work.Users.Get(1));
        }
        [Test]
        public void UpdateUser()
        {
            work.Users.Create(new User
            {
                Id = 2,
                Email = "babushekneobizhat@gmail.com",
                IsEmailVerified = true,
                UserName = "Lesha",
                UserPassword = "12345678",
                IsBanned = false,
            });
            User user = work.Users.Get(2);
            work.Users.Update(new User
            {
                Id = 2,
                Email = "babushekneobizhat@gmail.com",
                IsEmailVerified = true,
                UserName = "Lesha",
                UserPassword = "12345678",
                IsBanned = false,
            });
            Assert.AreNotEqual(user, work.Users.Get(2));
        }
        [Test]
        public void DeleteUser()
        {
            work.Users.Create(new User
            {
                Id = 3,
                Email = "babushekneobizhat@gmail.com",
                IsEmailVerified = true,
                UserName = "Lesha",
                UserPassword = "12345678",
                IsBanned = false,
            });
            work.Users.Delete(3);
            Assert.IsNull(work.Users.Get(3));
        }
    }
}
