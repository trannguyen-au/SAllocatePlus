using SwinSchool.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwinSchool.Services
{
    public static class Mapping
    {
        private static bool _isInitialized = false;
        public static void Initialize()
        {
            MapperConfig.RegisterConfiguration();
            _isInitialized = true;
        }

        public static T Map<T>(object input)
        {
            if (!_isInitialized)
                Initialize();
            return AutoMapper.Mapper.Map<T>(input);
        }
    }
}
