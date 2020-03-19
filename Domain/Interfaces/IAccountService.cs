using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Domain.DTO;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces
{
    public interface IAccountService
    {
        Task<UserDTO> RegisterRegularUser(UserDTO userDTO);
        Task<UserDTO> RegisterModerator(UserDTO userDTO);
        Task<IEnumerable<UserDTO>> GetAllRegularUsers();
        Task<IEnumerable<UserDTO>> GetAllModerators();
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<UserDTO> GetUserById(string id, string token);
        Task<bool> DeleteUser(string id, string token);
        Task UpdateUser(string id,UserDTO user,string token);
        Task<IEnumerable<OrderDTO>> GetAllOrdersByUserId(string id);
    }
}
