using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce.Models;

namespace E_commerce.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            Models.AppContext context = new Models.AppContext();

            int num_user = context.users.Count();
            int num_category = context.categories.Count();
            int num_order = context.orders.Count();
            int num_product = context.products.Count();

            ViewData["num_user"] = num_user;
            ViewData["num_category"] = num_category;
            ViewData["num_order"] = num_order;
            ViewData["num_product"] = num_product;

            return View();
        }
    }
}
