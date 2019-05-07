using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCloudBLL.Interfaces
{
   public interface IService<T> where T:class
    {
        void Make(T Element);
        void Change(T Element);
        void Delete(T Element);
        T GetElement(int? id);
        IEnumerable<T> GetElements();
        void Dispose();
    }
}
