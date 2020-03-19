using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string ProductName { get; set; }
        public short? UnitsOnOrder { get; set; }
        public decimal UnitPrice { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
        public ICollection<OrderDTO> Order { get; set; }
    }
}
