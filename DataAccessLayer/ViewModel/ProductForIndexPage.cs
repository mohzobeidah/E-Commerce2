using DataBaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.ViewModel
{
   public class ProductForIndexPage
    {
        public List<Product> products { get; set; } = new List<Product>();
    }
}
