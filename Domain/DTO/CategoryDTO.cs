using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string CategoryName { get; set; }
        [MaxLength(500)]
        [Required]
        public string Description { get; set; }
        public ICollection<ProductDTO> Products { get; private set; }
    }
}
