using ImageCloudDAL.EF;
using ImageCloudDAL.Entities;
using ImageCloudDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCloudDAL.Repositories
{
        public class UserRepository:IRepository<User>
    {
            private ImageContext db;

            public UserRepository(ImageContext context)
            {
                this.db = context;
            }

            public IEnumerable<User> GetAll()
            {
                return db.Users;
            }

            public User Get(int id)
            {
                return db.Users.Find(id);
            }

            public void Create(User element)
            {
                db.Users.Add(element);
            }

            public void Update(User element)
            {
                db.Entry(element).State = EntityState.Modified;
            }

            public IEnumerable<User> Find(Func<User, Boolean> predicate)
            {
                return db.Users.Where(predicate).ToList();
            }

            public void Delete(int id)
            {
                User element = db.Users.Find(id);
                if (element != null)
                    db.Users.Remove(element);
            }
        }
    
}
