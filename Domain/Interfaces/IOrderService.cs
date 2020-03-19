using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;

namespace Domain.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateOrder(OrderDTO orderdto);
        void DeleteOrder(int id, string token);
        void UpdateOrder(OrderDTO order, string token);
        OrderDTO GetOrderById(int id, string token);
        IEnumerable<OrderDTO> GetAllOrders();
        IEnumerable<ProductDTO> GetAllProductsByOrderId(int id, string token);
    }
}
