using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Entities
{
    public abstract class BaseEntity
    {
        public abstract int Id { get; set; }
    }
}
