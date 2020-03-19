using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entities
{
    public class Order : BaseEntity
    {
        public override int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime? OrderDate { get; set; }
        public ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
