using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAngular.Web.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public string SupplierId { get; set; }  
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public int UnitsOnStock { get; set; }
        public int UnitsOnOrder { get; set; }
        public bool Discontinued { get; set; } 
        public string PhotoFile { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}