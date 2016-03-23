using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwinSchool.Services
{
    public class ServiceFactory
    {
        public static T GetService<T>() where T :ServiceBase
        {
            if(typeof(T) == typeof(MyUserService))
            {
                return new MyUserService();
            }

            return null;
        }
    }
}
