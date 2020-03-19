using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Infrastructure.Entities
{
    public class User : IdentityUser
    {
        public string Address { get; set; }
        public string Phone { get; set; }
        public ICollection<Order> Orders { get; private set; }
    }
}
