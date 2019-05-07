using ImageCloudDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCloudDAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Image> Images { get; }
        IRepository<User> Users { get; }
        void Save();
    }
}
