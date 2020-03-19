using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Interfaces;
using Infrastructure.Entities;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly ApplicationDbContext _context;
        private ICategoryRepository _category;
        private IRepository<Product> _product;
        private IRepository<Order> _order;
        public ICategoryRepository Categories
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_context);
                }
                return _category;
            }
        }
        public IRepository<Product> Products
        {
            get
            {
                if (_product == null)
                {
                    _product = new Repository<Product>(_context);
                }
                return _product;
            }
        }
        public IRepository<Order> Orders
        {
            get
            {
                if (_order == null)
                {
                    _order = new Repository<Order>(_context);
                }
                return _order;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
