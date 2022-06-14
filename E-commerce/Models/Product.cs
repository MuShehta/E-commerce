using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int Cost { get; set; }
        public int stock { get; set; }
        public DateTime dateTime { get; set; }
        public string Description { get; set; }
        public List<Order> orders { get; set; }
        public List<Category> categories { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
