using System;
using System.Collections.Generic;
using System.Text;
using Domain.DTO;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        Task<object> Authenticate(UserDTO user);
    }
}
