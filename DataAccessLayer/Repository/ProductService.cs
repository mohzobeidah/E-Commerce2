using DataAccessLayer.IRepository;
using DataBaseLayer.Data;
using DataBaseLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Repository
{
   public class ProductService:Repository<Product>, IProductService
    {
        public ProductService(DataContext  dataContext):base(dataContext)
        {


        }

        public IQueryable<Product> getProductWithImage(int id )
        {
           var product= GetQueryable(p => p.IsDelete == false&& p.Id==id).Include(x => x.ProductPictures);
            return product;
        }

        public IQueryable<Product> GetPage(string filter, int initalPage, int pageSize, out int totalRecord, out int recordsFilter)
        {
              var data = GetQueryable(p => p.IsDelete == false).Include(x=>x.ProductPictures);
            totalRecord = GetQueryable(p => p.IsDelete == false).Count();

            if (!string.IsNullOrEmpty(filter))
            {
                data = data.Where(x =>
                x.Price.ToString().Contains(filter.ToLower()) ||
                x.EnglishName.ToLower().Contains(filter.ToLower()) ||
                x.ArabicName.ToLower().Contains(filter.ToLower()) ||
                x.FrenchName.ToLower().Contains(filter.ToLower())
                ).Include(x => x.ProductPictures);
            }
            recordsFilter = data.Count();
            var data2 = data.OrderByDescending(x => x.InsertDate).Skip(initalPage).Take(pageSize);
            return data2;

        }

    }


}
