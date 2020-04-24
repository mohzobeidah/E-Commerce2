using DataAccessLayer.IRepository;
using DataBaseLayer.Data;
using DataBaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


namespace DataAccessLayer.Repository
{
    public class ProductPictureService : Repository<ProductPicture>, IProductPictureService
    {
        public ProductPictureService(DataContext dataContext):base(dataContext)
        {
            
        }
        public void dd()
        {
            // var imgFile = Image.FromFile("wwwroot\\Images\\picture.jpg");

           
        }
    }
}
