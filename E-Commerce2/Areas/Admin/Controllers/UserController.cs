using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.IRepository;
using E_Commerce2.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult ShowUser()
        {
            return View();

        }
        [HttpPost]
        public IActionResult GetUser(DataTablesParam param)
        {
            int totalNo = 0, recordFilter = 0;
            var productList = userService.GetPage(
                                    param.sSearch,
                                    param.iDisplayStart,
                                    param.iDisplayLength,
                                    out totalNo,
                                    out recordFilter
                                    );
            //var newProductList = productList.Select(x => new
            //{
            //    id = x.Id,
            //    name = x.EnglishName,
            //    picPath = x.ProductPictures.Where(y => y.DefaultPicture == true).FirstOrDefault().Name,
            //    price = x.Price
            //});


            return Json(new
            {
                data = productList,
                eEcho = param.sEcho,
                iTotalDisplayRecords = recordFilter,
                iTotalRecords = totalNo
            });
        }
    }
}