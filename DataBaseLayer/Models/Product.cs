using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataBaseLayer.Models
{
   public class Product
    {
        public int Id { get; set; }
        //[Column(TypeName = "VARCHAR(250)")]
          
        public String ArabicName { get; set; }
        //[Column(TypeName = "VARCHAR(250)")]
          
        public String FrenchName { get; set; }
        //[Column(TypeName = "VARCHAR(250)")]
          
        public String EnglishName { get; set; }
        public int Quantity { get; set; }

        public float Price { get; set; }
        public float Disaccunt { get; set; }
        //[Column(TypeName = "VARCHAR(250)")]
          
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        //[Column(TypeName = "VARCHAR(250)")]
          
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDelete { get; set; }
        //[Column(TypeName = "VARCHAR(250)")]
          
        public String IPAdress { get; set; }
        //[Column(TypeName = "VARCHAR(250)")]
          
        public string ProductImageLink { get; set; }
        //[Column(TypeName = "VARCHAR(250)")]
          
        public string ProductImageName { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category category { get; set; }

    }
}
