using DataAccessLayer.IRepository;
using DataBaseLayer.Data;
using DataBaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repository
{
   public class ProductService:Repository<Product>, IProductService
    {
        public ProductService(DataContext  dataContext):base(dataContext)
        {


        }
    }
    
    
}
