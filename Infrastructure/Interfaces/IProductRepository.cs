using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Entities;

namespace Infrastructure.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        //Product GetProductByCategoryId(int id);
    }
}
