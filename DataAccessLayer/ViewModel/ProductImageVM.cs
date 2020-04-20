using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.ViewModel
{
   public class ProductImageVM
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string size { get; set; }
        public string Extenstionsfile { get; set; }
        public bool DefaultPicture { get; set; }


        public int ProductId { get; set; }
        

        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }

        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDelete { get; set; }
       
    }
}
