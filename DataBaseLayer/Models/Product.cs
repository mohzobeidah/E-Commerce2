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
  
        
        public bool IsAvaible { get; set; }
        public bool IsShpping { get; set; }
        public bool IsView { get; set; }

        public string ArImportantDetails { get; set; }
        public string EnImportantDetails { get; set; }
        public string FrImportantDetails { get; set; }

        public string EnglishDetails { get; set; }
        public string ArabicDetails { get; set; }
        public string FrenchDetails { get; set; }



        //[Column(TypeName = "VARCHAR(250)")]

        public String IPAdress { get; set; }
        //[Column(TypeName = "VARCHAR(250)")]
          
       // public string ProductImageLink { get; set; }
        //[Column(TypeName = "VARCHAR(250)")]
          
       // public string ProductImageName { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category category { get; set; }


        public ICollection<ProductPicture> ProductPictures { get; set; }

        public ICollection<Tag_Product> tag_Products { get; set; }

        /// <summary>
        /// ///////////////////////////////////
        /// </summary>

        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        //[Column(TypeName = "VARCHAR(250)")]

        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDelete { get; set; }

    }
}
