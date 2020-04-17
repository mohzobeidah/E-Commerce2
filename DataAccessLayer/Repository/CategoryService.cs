using DataAccessLayer.IRepository;
using DataBaseLayer.Data;
using DataBaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Repository
{
   public class CategoryService : Repository<Category>, ICategoryService
    {
        public CategoryService(DataContext dataContext):base(dataContext)
        {
         
        }
        public IQueryable<Category> GetPaginated(string filter, int initalPage, int pageSize, out int totalRecord, out int recordsFilter)
            {


                var data = GetQueryable(x => x.IsDelete == false);
                totalRecord = GetQueryable(x => x.IsDelete == false).Count();
                if (!string.IsNullOrEmpty(filter))
                {

                    data = data.Where(x => x.FrenchName.Contains(filter.ToLower()) ||
                    x.ArabicName.ToLower().Contains(filter.ToLower()) || x.EnglishName.ToLower().Contains(filter.ToLower())
                    );
                }

                recordsFilter = data.Count();
                data = data.OrderByDescending(x => x.InsertDate).Skip(initalPage).Take(pageSize);
                return data;
            }
    }
}
