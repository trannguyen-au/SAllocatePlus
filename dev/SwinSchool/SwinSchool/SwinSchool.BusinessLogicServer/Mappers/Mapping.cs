using SwinSchool.BusinessLogicServer.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwinSchool.BusinessLogicServer
{
    public static class Mapping
    {
        private static bool _isInitialized = false;
        public static void Initialize()
        {
            MapperConfig.RegisterConfiguration();
            _isInitialized = true;
        }

        public static T Map<T,T2>(T2 input) where T2: class 
                                            where T : class
        {
            if (!_isInitialized)
                Initialize();
            return AutoMapper.Mapper.Map<T2, T>(input);
        }
    }
}
