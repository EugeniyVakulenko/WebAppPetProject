using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entities
{
    public class Product : BaseEntity
    {
        public override int Id { get; set; }
        public string ProductName { get; set; }
        public short? UnitsOnOrder { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
