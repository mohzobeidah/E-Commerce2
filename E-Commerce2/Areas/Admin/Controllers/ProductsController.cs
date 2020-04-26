using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Enum;
using DataAccessLayer.IRepository;
using DataAccessLayer.ViewModel;
using DataBaseLayer.Models;
using E_Commerce2.Controllers;
using E_Commerce2.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce2.Areas.Admin.Controllers
{
    [Area("Admin")]


    public class ProductsController : BaseController
    {
        private readonly IHttpContextAccessor accessor;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IHostingEnvironment env;
        private readonly IMapper mapper;
        private readonly IProductPictureService productPictureService;

        public ProductsController(IHttpContextAccessor accessor ,IProductService productService ,ICategoryService categoryService
            , IHostingEnvironment env, IMapper mapper, IProductPictureService productPictureService) :base(accessor)


        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.env = env;
            this.mapper = mapper;
            this.productPictureService = productPictureService;
        }
        public IActionResult Products()
        {
            return View();
        }


        [HttpPost]
        public IActionResult GetProduct(DataTablesParam param)
        {
            int totalNo = 0, recordFilter = 0;
            var productList = productService.GetPage(
                                    param.sSearch,
                                    param.iDisplayStart,
                                    param.iDisplayLength,
                                    out totalNo,
                                    out recordFilter
                                    );
          var newProductList = productList.Select(x => new
            { id= x.Id,
                name = x.EnglishName,
                picPath = x.ProductPictures.Where(y => y.DefaultPicture == true).FirstOrDefault().Name,
                price = x.Price
            });
            
                    
            return Json(new
            {
                data = newProductList,
                eEcho = param.sEcho,
                iTotalDisplayRecords = recordFilter,
                iTotalRecords = totalNo
            });
        }
        public IActionResult AddNewProduct(int id)
        
        {
            if (id==0)
            {
            var product = new ProductVM
            {
                categoryList = new SelectList(categoryService.GetQueryable(x => x.IsDelete == false), "Id", "EnglishName"),
            };
            return View(product);
            }
            else
            {

                var getProduct = productService.getProductWithImage(id);

            //     var newProductList = getProduct.Select(x => new
            //    {
            //        id = x.Id,
            //         englishName = x.EnglishName,
            //         arabicName = x.ArabicName,
            //         frenchName = x.FrenchName,
            //         picPath = x.ProductPictures.Where(y => y.DefaultPicture == true).FirstOrDefault().Name,
            //         price = x.Price,
            //         quantity = x.Quantity,
            //         disaccunt = x.Disaccunt,
            //         categoryList = new SelectList(categoryService.GetQueryable(p => p.IsDelete == false), "Id", "EnglishName"),

            //});
                var product = getProduct.FirstOrDefault();
                var productvm = mapper.Map<ProductVM>(product);

                productvm.categoryList = new SelectList(categoryService.GetQueryable(x => x.IsDelete == false), "Id", "EnglishName");
                
               
                return View(productvm);
            }
            



        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<JsonResult> AddNewProduct(ProductVM model)
        {
            if (model.Id == 0)
            {

                if (ModelState.IsValid)
                {
                    String UniqueFileName = null;
                    if (model.files != null)
                    {
                        string FileNameUploader = Path.Combine(env.WebRootPath, "ProductImage");
                        UniqueFileName = Guid.NewGuid().ToString() + "_" + model.files.FileName;
                        string FilePath = Path.Combine(FileNameUploader, UniqueFileName);
                        using (var copyfile = new FileStream(FilePath, FileMode.Create))
                        {
                            model.files.CopyTo(copyfile);
                        }


                        model.ProductPictures.Add(new ProductPicture
                        {
                            DefaultPicture = true,
                            Name = UniqueFileName,
                            size = model.files.Length.ToString(),
                            Extenstionsfile = model.files.ContentType
                        });
                    }

                    model.InsertDate = DateTime.UtcNow;
                    model.InsertUser = USERNAME;
                    var newObjectForSave = mapper.Map<Product>(model);
                    var result = await productService.AddAndLogAsync(newObjectForSave, USERNAME);
                    if (result > 0)
                    {
                        return Json(new
                        {
                            status = JsonStatus.Success,
                            link = "جيد",
                            color = NotificationColor.success.ToString().ToLower(),
                            msg = "تم الحفظ نجاح",
                            ObjectID= newObjectForSave.Id

                        });
                    }
                    else
                    {

                        return Json(new
                        {
                            status = JsonStatus.Error,
                            link = "يوجد خطا",
                            color = NotificationColor.error.ToString().ToLower(),
                            ObjectID = newObjectForSave.Id,
                            msg = "يوجد خطا في عملية الحفظ",
                        });

                    }
                }

                return Json(new
                {
                    status = JsonStatus.Error,
                    link = "",
                    color = NotificationColor.error.ToString().ToLower(),
                    msg = ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                                 .Select(m => m.ErrorMessage).ToArray()
                });
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var getProduct = await productService.GetAsync(x => x.Id == model.Id);

                   var product = getProduct.FirstOrDefault();

                    product.FrenchName = model.FrenchName;
                    product.ArabicName = model.ArabicName;
                    product.EnglishName = model.EnglishName;

                    product.ArabicDetails = model.ArabicDetails;
                    product.EnglishDetails = model.EnglishDetails;
                    product.FrenchDetails = model.FrenchDetails;

                    product.ArImportantDetails = model.ArImportantDetails;
                    product.EnImportantDetails = model.EnImportantDetails;
                    product.FrImportantDetails = model.FrImportantDetails;


                    product.Quantity = model.Quantity;
                    product.Price = model.Price;
                    product.Disaccunt = model.Disaccunt;

                    product.IsAvaible = model.IsAvaible;
                    product.IsShpping = model.IsShpping;

                    product.CategoryId = model.CategoryId;

                    product.UpdateUser = USERNAME;
                    product.UpdateDate = DateTime.UtcNow;


                    var result = await productService.UpdateAndLogAsync(product, USERNAME);
                    if (result > 0)
                    {
                        return Json(new
                        {
                            status = JsonStatus.Success,
                            link = "جيد",
                            color = NotificationColor.success.ToString().ToLower(),
                            msg = "تم الحفظ نجاح",
                            ObjectID = model.Id

                        });
                    }
                    else
                    {

                        return Json(new
                        {
                            status = JsonStatus.Error,
                            link = "يوجد خطا",
                            color = NotificationColor.error.ToString().ToLower(),
                            ObjectID = model.Id,
                            msg = "يوجد خطا في عملية الحفظ",
                        });

                    }

                }
                return Json(new
                {
                    status = JsonStatus.Error,
                    link = "",
                    color = NotificationColor.error.ToString().ToLower(),
                    msg = ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                                   .Select(m => m.ErrorMessage).ToArray()
                });
            }
        }
        


        [HttpPost]

        public async Task<JsonResult> SaveGallery(IFormFile file, int productId)
        {
           if (file !=null  && productId >0 ) { 
            String UniqueFileName = null;
            if (file != null)
            {
                string FileNameUploader = Path.Combine(env.WebRootPath, "ProductImage");
                UniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string FilePath = Path.Combine(FileNameUploader, UniqueFileName);
                using (var copyfile = new FileStream(FilePath, FileMode.Create))
                {
                    file.CopyTo(copyfile);
                }


            }
            var pic = new ProductPicture {

                DefaultPicture = true,
                Name = UniqueFileName,
                size = file.Length.ToString(),
                Extenstionsfile = file.ContentType,
                ProductId=productId,
                InsertUser=USERNAME,
                InsertDate=DateTime.UtcNow
                
            };
           var result= await productPictureService.AddAndLogAsync(pic, USERNAME);
          // var filename = productPictureService.GetQueryable(x => x.Id == productId).FirstOrDefault().Name;
                
            if (result > 0)
            {
                return Json(new
                {
                    status = JsonStatus.Success,
                    link = "جيد",
                    color = NotificationColor.success.ToString().ToLower(),
                    msg = "تم الحفظ نجاح",
                    ObjectID = result,
                    filename= pic.Name


                });
            }
            else
            {
                return Json(new
                {
                    status = JsonStatus.Error,
                    link = "يوجد خطا",
                    color = NotificationColor.error.ToString().ToLower(),
                    ObjectID = result,
                    msg = "يوجد خطا في عملية الحفظ",
                    filename= pic.Name
                });

            }
        }
                return Json(new
                {
                    status = JsonStatus.Error,
                    link = "",
                    color = NotificationColor.error.ToString().ToLower(),
                    msg = "يوجد خطا في عملية الحفظ",
                });

        }

        [HttpPost]

        public async Task<JsonResult> deleteImage(string id)
        {
            if (id!= null) { 
            var file = productPictureService.GetQueryable(x => x.Name.ToString() == id).FirstOrDefault();
                int result;
                if (file != null)
                {
                    file.IsDelete = true;
                    file.UpdateUser = USERNAME;
                    file.UpdateDate = DateTime.UtcNow;

                    result = await productPictureService.UpdateAndLogAsync(file, USERNAME);
                  
                    string FileNameUploader = Path.Combine(env.WebRootPath, "ProductImage");
                   
                    string FilePath = Path.Combine(FileNameUploader, file.Name);
                    System.IO.File.Delete(FilePath);
                    result = 1;
                }
                else
                    result = 0;

                if (result > 0)
                {
                    return Json(new
                    {
                        status = JsonStatus.Success,
                        link = "جيد",
                        color = NotificationColor.success.ToString().ToLower(),
                        msg = "تم الازالة نجاح",
                       


                    });
                }
                else
                {
                    return Json(new
                    {
                        status = JsonStatus.Error,
                        link = "يوجد خطا",
                        color = NotificationColor.error.ToString().ToLower(),
                        ObjectID = result,
                        msg = "يوجد خطا في عملية الازالة",
                    
                    });

                }
            }
            return Json(new
            {
                status = JsonStatus.Error,
                link = "يوجد خطا",
                color = NotificationColor.error.ToString().ToLower(),
                msg = "يوجد خطا في عملية الازالة"
            });

        }
    }
}