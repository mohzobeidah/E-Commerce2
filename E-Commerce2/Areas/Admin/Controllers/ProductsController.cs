using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce2.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductsController : Controller
    {
        public IActionResult Products()
        {
            return View();
        }

        public IActionResult AddNewProduct()
        {
            return View();
        }
    }
}