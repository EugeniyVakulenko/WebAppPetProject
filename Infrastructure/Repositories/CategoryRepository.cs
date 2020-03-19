using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public ApplicationDbContext Context
        {
            get { return _context as ApplicationDbContext; }
        }

        public Category GetByIdWithProducts(int id)
        {
            return Context.Categories.Where(i => i.Id == id).
                Include(p => p.Products).FirstOrDefault();
        }
        public IEnumerable<Category> GetAllWithProducts()
        {
            return Context.Categories.Include(p => p.Products).ToList();
        }
    }
}
