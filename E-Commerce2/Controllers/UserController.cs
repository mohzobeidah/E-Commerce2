using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce2.Controllers
{
    public class UserController : Controller
    {
        public IActionResult ShowUser()
        {
            return View();
        }
    }
}