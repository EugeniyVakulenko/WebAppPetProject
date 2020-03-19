using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entities
{
    public class Category : BaseEntity
    {
        public override int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; private set; }
    }
}
