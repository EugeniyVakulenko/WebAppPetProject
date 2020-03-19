using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
        public UserDTO User { get; set; }
        public DateTime? OrderDate { get; set; }
        public ICollection<ProductDTO> Product { get; set; }
    }
}
