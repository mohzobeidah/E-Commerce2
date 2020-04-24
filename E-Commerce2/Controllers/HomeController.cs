using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_Commerce2.Models;
using Serilog.Core;
using Microsoft.Extensions.Logging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using LazZiya.ImageResize;
using DataAccessLayer.IRepository;
using System.Security.Cryptography.X509Certificates;
using DataAccessLayer.ViewModel;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using DataBaseLayer.Models;
using System.Xml.Schema;
using Microsoft.AspNetCore.Routing;
using E_Commerce2.Helper;

namespace E_Commerce2.Controllers
{
 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> Logger;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public HomeController(ILogger<HomeController> _logger ,IProductService productService, ICategoryService categoryService)
        {
            Logger = _logger;
            this.productService = productService;
            this.categoryService = categoryService;
        }
        public IActionResult Index()
        {

            var product = productService.GetQueryable(x => x.IsDelete == false).Include(x=>x.ProductPictures).OrderByDescending(x => x.Id).Take(20);

            var dataForindex = new ProductForIndexPage();
            dataForindex.products = product.ToList();


            return View(dataForindex);
        }

        public async Task<IActionResult>  ShowProduct(string sortOrder,
    string currentFilter,
    string searchString,
    int? pageNumber,int? CategoryId)
        
        
        
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["category"] = await categoryService.GetQueryable(x => x.IsDelete == false).ToListAsync();
            ViewData["categoryId"] = CategoryId;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var product = productService.GetAllQuerable();
            if (CategoryId !=null && CategoryId!=0)
            {
                product = productService.GetQueryable(x=>x.CategoryId==CategoryId).Include(x=>x.ProductPictures);


            }

            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.EnglishName.Contains(searchString)
                                       || s.Price.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    product = product.OrderByDescending(s => s.Price);
                    break;
                case "Date":
                    product = product.OrderBy(s => s.EnglishName);
                    break;
                case "date_desc":
                    product = product.OrderByDescending(s => s.CategoryId);
                    break;
                default:
                    product = product.OrderByDescending(s => s.InsertDate);
                    break;
            }

            int pageSize = 12;
            return View(await PaginatedList<Product>.CreateAsync(product.Include(x => x.ProductPictures).AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> GetProduct()
        {


            var product = productService.GetQueryable(x => x.IsDelete == false).Include(x => x.ProductPictures).OrderByDescending(x => x.Id);
           

            return View(product);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }






      
    }
}
