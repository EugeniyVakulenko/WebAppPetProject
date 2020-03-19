using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression(@"^.*(?=.{6,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(^[a-zA-Z0-9@\$=!:.#%]+$)")]
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public ICollection<OrderDTO> Orders { get; private set; }
    }
}
