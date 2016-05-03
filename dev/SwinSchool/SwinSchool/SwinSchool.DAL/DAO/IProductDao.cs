using SwinSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwinSchool.DAL.DAO
{
    public interface IProductDao
    {
        List<Product> GetAll();
        Product GetById(string userId);
        
    }
}
