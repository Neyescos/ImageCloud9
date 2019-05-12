using ImageCloudBLL.DTO;
using ImageCloudBLL.Interfaces;
using ImageCloudBLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageCloud.Util
{
    public class UserModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IService<UserDTO>>().To<UserService>();
        }
    }
}