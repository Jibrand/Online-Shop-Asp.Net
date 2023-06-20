using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Project.Models
{
    public class cart
    {
        public int id { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public float bill { get; set; }
        public int qty { get; set; }


        public HttpPostedFileBase ImageFile { get; set; }
    }
}