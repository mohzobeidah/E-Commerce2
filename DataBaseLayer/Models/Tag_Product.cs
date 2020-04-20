using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataBaseLayer.Models
{
  public class Tag_Product
    {



        public int Id { get; set; }
        public int IdTag { get; set; }
        public int IdProduct { get; set; }


    
        [ForeignKey(nameof(IdTag))]
        public Tag Tag { get; set; }

        [ForeignKey(nameof(IdProduct))]
        public Product Product { get; set; }

        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        //[Column(TypeName = "VARCHAR(250)")]

        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDelete { get; set; }



    }
}
