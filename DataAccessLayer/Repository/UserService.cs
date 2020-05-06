using DataAccessLayer.IRepository;
using DataBaseLayer.Data;
using DataBaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Repository
{
   public class UserService : Repository<User>,IUserService
    {
        public UserService(DataContext dataContext) : base(dataContext)
        {


        }
        public IQueryable<User> GetPage(string filter, int initalPage, int pageSize, out int totalRecord, out int recordsFilter)
        {
            var data = GetAllQuerable();
            totalRecord = GetAllQuerable().Count();

            if (!string.IsNullOrEmpty(filter))
            {
                //data = data.Where(x =>
                //x.Price.ToString().Contains(filter.ToLower()) ||
                //x.EnglishName.ToLower().Contains(filter.ToLower()) ||
                //x.ArabicName.ToLower().Contains(filter.ToLower()) ||
                //x.FrenchName.ToLower().Contains(filter.ToLower())
                //).Include(x => x.ProductPictures);
            }
            recordsFilter = data.Count();
            var data2 = data.OrderByDescending(x => x.Id).Skip(initalPage).Take(pageSize);
            return data2;

        }
    }
}
