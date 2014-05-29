using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcAngular.Web.Repository;

namespace MvcAngular.Web.Models
{
    public class ProductResponse
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int Records { get; set; }
        public List<Product> Rows { get; set; }
    }
}