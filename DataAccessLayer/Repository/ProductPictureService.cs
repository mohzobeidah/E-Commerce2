using DataAccessLayer.IRepository;
using DataBaseLayer.Data;
using DataBaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repository
{
    public class ProductPictureService : Repository<ProductPicture>, IProductPictureService
    {
        public ProductPictureService(DataContext dataContext):base(dataContext)
        {

        }
    }
}
