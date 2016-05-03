using SwinSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwinSchool.DAL.DAO.Impl
{
    public class ProductDao : IProductDao
    {
        private SchoolContext _context;
        public ProductDao()
        {
            _context = new SchoolContext();
        }
        public List<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Entities.Product GetById(string productId)
        {
            return _context.Products.FirstOrDefault(p => p.ProductID == productId);
        }
    }
}
