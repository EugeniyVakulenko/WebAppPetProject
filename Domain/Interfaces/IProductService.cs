using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;

namespace Domain.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> CreateProduct(ProductDTO productdto);
        void DeleteProduct(int id);
        void UpdateProduct(ProductDTO productdto);
        ProductDTO GetProductById(int id);
        IEnumerable<ProductDTO> GetAllProducts();
        IEnumerable<OrderDTO> GetOrdersByProductId(int id);
    }
}
