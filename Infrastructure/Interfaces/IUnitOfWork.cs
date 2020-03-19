using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Categories { get; }
        public IRepository<Order> Orders { get; }
        public IRepository<Product> Products { get; }
        void Save();
        Task SaveAsync();
    }
}
