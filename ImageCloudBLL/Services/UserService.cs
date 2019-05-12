using ImageCloudBLL.DTO;
using ImageCloudBLL.Infrastructure;
using ImageCloudBLL.Interfaces;
using ImageCloudDAL.Entities;
using ImageCloudDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCloudBLL.Services
{
    public class UserService : IService<UserDTO>
    {
        IUnitOfWork Database { get; set; }
        Mailer mail = new Mailer();
        public  UserService (IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Change(UserDTO Element)
        {
            Database.Users.Update(new User { Id = Element.Id, Email = Element.Email, IsBanned = Element.IsBanned, IsEmailVerified = Element.IsEmailVerified, UserName = Element.UserName, UserPassword = Element.Password, UserRole = Element.UserRole });
            Database.Save();
        }

        public void Delete(UserDTO Element)
        {

            Database.Users.Delete(Element.Id);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public UserDTO GetElement(int? id)
        {
            if (id == null)
            {
                throw new ValidationException("id не может быть null", "");
            }
            var element = Database.Users.Get(id.Value);
            if (element != null)
            {
                return new UserDTO { Id = element.Id, Email = element.Email, IsBanned = element.IsBanned, IsEmailVerified = element.IsEmailVerified, Password = element.UserPassword, UserName = element.UserName, UserRole = element.UserRole };
            }
            throw new ValidationException("Не существует елемента с данным id", "");
        }
        public IEnumerable<UserDTO> GetElements()
        {
            List<UserDTO> users = new List<UserDTO>();
            foreach (var element in Database.Users.GetAll())
            {
                users.Add(new UserDTO { Email = element.Email, Id = element.Id, IsBanned = element.IsBanned, IsEmailVerified = element.IsEmailVerified, Password = element.UserPassword, UserName = element.UserName, UserRole = element.UserRole });

            }
            return users;
        }

        public  void Make(UserDTO Element)
        {
            User user = new User { Email = Element.Email, Id = Element.Id, UserRole = Element.UserRole, IsBanned = Element.IsBanned, IsEmailVerified = false, UserName = Element.UserName, UserPassword = Element.Password };
            Database.Users.Create(user);
            
            Database.Save();
            Func<User, bool> f = m => m.Email == Element.Email;
            mail.SendMail(Database.Users.Find(f).First());
        }

        public UserDTO Find(Func<User, bool> predicate)
        {
            var user = Database.Users.Find(predicate).First();
            return new UserDTO { Email = user.Email, Id = user.Id, IsBanned = user.IsBanned, IsEmailVerified = user.IsEmailVerified, Password = user.UserPassword, UserName = user.UserName, UserRole = user.UserRole };
        }
    }
}
