using System;
using System.Collections.Generic;
using System.Text;
using Domain.Interfaces;
using Infrastructure.Interfaces;
using AutoMapper;
using Domain.DTO;
using System.Threading.Tasks;
using Infrastructure.Entities;

namespace Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ProductDTO> CreateProduct(ProductDTO productdto)
        {
            if (productdto == null) throw new ArgumentNullException(nameof(productdto));
            var product = _mapper.Map<Product>(productdto);
            _unitOfWork.Products.Add(product);
            await _unitOfWork.SaveAsync();
            var productEntity = _unitOfWork.Products.Find(i => i.Id == productdto.Id);
            if (productEntity == null) throw new ArgumentNullException(nameof(productEntity));
            return _mapper.Map<ProductDTO>(productEntity);
        }
        public void DeleteProduct(int id)
        {
            var productEntity = _unitOfWork.Products.GetById(id);
            if (productEntity == null) throw new ArgumentNullException(nameof(productEntity), "This product doesn't exist");
            _unitOfWork.Products.Delete(productEntity);
            _unitOfWork.Save();
        }
        public void UpdateProduct(ProductDTO productdto)
        {
            if (productdto == null) throw new ArgumentNullException(nameof(productdto));
            var product = _mapper.Map<Product>(productdto);

            _unitOfWork.Products.Update(product);
            _unitOfWork.Save();
        }
        public ProductDTO GetProductById(int id)
        {
            var product = _unitOfWork.Products.Find(i => i.Id == id);
            return _mapper.Map<ProductDTO>(product);
        }
        public IEnumerable<ProductDTO> GetAllProducts()
        {
            var products = _unitOfWork.Products.GetAll();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }
        public IEnumerable<OrderDTO> GetOrdersByProductId(int id)
        {
            var product = GetProductById(id);
            return product.Order;
        }

    }
}
