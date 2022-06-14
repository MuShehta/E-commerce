using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(11)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        [MinLength(6),MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public bool Is_admin { get; set; }
        public List<Order> orders { get; set; }
    }
}
