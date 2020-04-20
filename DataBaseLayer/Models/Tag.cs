using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.Models
{
    public class Tag
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        //[Column(TypeName = "VARCHAR(250)")]

        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<Tag_Product> tag_Products { get; set; }
    }
}
