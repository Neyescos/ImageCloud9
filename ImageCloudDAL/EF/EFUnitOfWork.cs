using ImageCloudDAL.Entities;
using ImageCloudDAL.Interfaces;
using ImageCloudDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCloudDAL.Repositories;

namespace ImageCloudDAL.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private ImageContext db;
        private ImageRepository imageRepository;
        private UserRepository userRepository;

        public EFUnitOfWork()
        {
            db = new ImageContext();
        }
        public EFUnitOfWork(string connectionString)
        {
            db = new ImageContext(connectionString);
        }
        public IRepository<Image> Images
        {
            get
            {
                if (imageRepository == null)
                    imageRepository = new ImageRepository(db);
                return imageRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
