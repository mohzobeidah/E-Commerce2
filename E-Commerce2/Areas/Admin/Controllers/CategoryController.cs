using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataAccessLayer.Enum;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using DataAccessLayer.ViewModel;
using DataBaseLayer.Models;
using E_Commerce2.Controllers;
using E_Commerce2.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce2.Areas.Admin.Controllers
{
    [Area("Admin")]
     [Authorize]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(IHttpContextAccessor accessor, ICategoryService categoryService, IMapper mapper) : base(accessor)
        {
            this._categoryService = categoryService;
            _mapper = mapper;
        }

        public IActionResult Categories()
        {


            return View();
        }
        [HttpPost]

        public IActionResult GetCategoriesAjax(DataTablesParam param)
        {
            int totalNo = 0, recordFilter = 0;
            var categoryLIST = _categoryService.GetPaginated(param.sSearch, param.iDisplayStart, param.iDisplayLength, out totalNo, out recordFilter);
            return Json(new
            {
                data = categoryLIST,
                eEcho = param.sEcho,
                iTotalDisplayRecords = recordFilter,
                iTotalRecords = totalNo
            });
        }

        public async Task<IActionResult> _Add()
        {
          //  _categoryService.AddAndLogAsync()

            return PartialView("_Add");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task <JsonResult> AddCategories(CategoryVM model)
        {
            if (ModelState.IsValid)
            {
                model.InsertUser = USERNAME;
                model.InsertDate = DateTime.UtcNow;
                model.IPAdress =IpAddresss;
                var result = await _categoryService.AddAndLogAsync(_mapper.Map<Category>(model),USERNAME);

                if (result > 0)
                {
                    return Json(new
                    {
                        status = JsonStatus.Success,
                        link = "جيد",
                        color = NotificationColor.success.ToString().ToLower(),
                        msg = "تم الحفظ نجاح"
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = JsonStatus.Error,
                        link = "يوجد خطا",
                        color = NotificationColor.error.ToString().ToLower(),
                        msg = "يوجد خطا في عملية الحفظ"
                    });

                }
            }

            // var errors = ModelState.Keys.Where(k => ModelState[k].Errors.Count > 0).Select(k => new { propertyName = k, errorMessage = ModelState[k].Errors[0].ErrorMessage });
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
}