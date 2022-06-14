using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public class Order
    {

        public int Id { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public User user { get; set; }
        public List<Product> products { get; set; }

    }
}
