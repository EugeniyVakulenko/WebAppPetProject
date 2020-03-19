using System;
using System.Collections.Generic;
using System.Text;
using Domain.Interfaces;
using Microsoft.AspNetCore;
using Infrastructure.Interfaces;
using AutoMapper;
using Domain.DTO;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Domain.Exceptions;

namespace Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtFactory _jwtFactory;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IJwtFactory jwtFactory)
        {
            _unitOfWork = unitOfWork;
            _jwtFactory = jwtFactory;
            _mapper = mapper;
        }
        public async Task<OrderDTO> CreateOrder(OrderDTO orderdto)
        {
            if (orderdto == null) throw new ArgumentNullException(nameof(orderdto));
            var order = _mapper.Map<Order>(orderdto);
            var orderproducts = new List<OrderProduct>();
            foreach (var item in orderdto.Product)
            {
                var product = _unitOfWork.Products.GetById(item.Id);
                orderproducts.Add(new OrderProduct
                {
                    Order = order,
                    Product = product
                });
            }
            order.OrderProduct = orderproducts;
            _unitOfWork.Orders.Add(order);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<OrderDTO>(order);
        }
        public void DeleteOrder(int id, string token)
        {
            var order = _unitOfWork.Orders.GetById(id);
            if (order == null) throw new ArgumentNullException(nameof(order), "Order wasn't found");
            string claimsId = _jwtFactory.GetUserIdClaim(token);
            if (claimsId == order.UserId)
            {
                _unitOfWork.Orders.Delete(order);
                _unitOfWork.Save();
            }
            string claimsRole = _jwtFactory.GetUserRoleClaim(token);
            if (claimsRole == "Moderator" || claimsRole == "Admin")
            {
                _unitOfWork.Orders.Delete(order);
                _unitOfWork.Save();
            }
            else throw new NotEnoughRightsException();
        }
        public void UpdateOrder(OrderDTO order, string token)
        {
            var orderEntity = _mapper.Map<Order>(order);
            if (orderEntity == null) throw new ArgumentNullException(nameof(orderEntity));
            string claimsId = _jwtFactory.GetUserIdClaim(token);
            if (claimsId == order.UserId)
            {
                _unitOfWork.Orders.Update(orderEntity);
                _unitOfWork.Save();
            }
            _unitOfWork.Orders.Update(orderEntity);
            _unitOfWork.Save();
        }
        public OrderDTO GetOrderById(int id, string token)
        {
            var order = _unitOfWork.Orders.GetById(id);
            string claimsId = _jwtFactory.GetUserIdClaim(token);
            if (claimsId == order.UserId)
            {
                return _mapper.Map<OrderDTO>(order);
            }
            string claimsRole = _jwtFactory.GetUserRoleClaim(token);
            if(claimsRole == "Admin" || claimsRole == "Moderator")
            {
                return _mapper.Map<OrderDTO>(order);
            }
            else throw new NotEnoughRightsException();
        }
        public IEnumerable<OrderDTO> GetAllOrders()
        {
            var orders = _unitOfWork.Orders.GetAll();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public IEnumerable<ProductDTO> GetAllProductsByOrderId(int id, string token)
        {
            var order = GetOrderById(id,token);
            return order.Product;
        }
    }
}
