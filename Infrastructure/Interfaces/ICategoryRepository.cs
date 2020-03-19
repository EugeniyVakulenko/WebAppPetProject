using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Entities;

namespace Infrastructure.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByIdWithProducts(int id);
        IEnumerable<Category> GetAllWithProducts();
    }
}
