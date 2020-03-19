using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Interfaces;
using Infrastructure.Context;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public ApplicationDbContext Context
        {
            get { return _context as ApplicationDbContext; }
        }

        //public Product GetProductByCategoryId(int id)
        //{
        //    return Context.Products.Where(c => c.CategoryId == id).FirstOrDefault();
        //}
        public IEnumerable<Product> GetAllWithOrders()
        {
            return Context.Products
                .Include(op => op.OrderProduct)
                .ThenInclude(o => o.Product).ToList();
        }
    }
}
