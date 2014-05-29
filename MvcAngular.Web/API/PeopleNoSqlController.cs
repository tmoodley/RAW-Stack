using MvcAngular.Web.Models;
using Raven.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MvcAngular.Web.API
{
    public class PeopleNoSqlController : RavenDbController
    {  
        public void Put()
        {
            var product = new Product
            {
                Name = "Product Name",
                CategoryId = "category/1024",
                SupplierId = "supplier/16",
                Code = "H11050",
                CreatedAt = DateTime.Now,
                StandardCost = 250,
                ListPrice = 189,
                PhotoFile = "path to picture.jpg",
            };
            Session.StoreAsync(product);
            Session.SaveChangesAsync();  
        }

        public string Get()
        {

            var sampleData = new SampleData { CreatedAt = SystemTime.UtcNow, Name = "teest" };
            Session.StoreAsync(sampleData);


            var product = new Product
            {
                Name = "Product Name",
                CategoryId = "category/1024",
                SupplierId = "supplier/16",
                Code = "H11050",
                CreatedAt = DateTime.Now,
                StandardCost = 250,
                ListPrice = 189,
                PhotoFile = "path to picture.jpg",
            };
            Session.StoreAsync(product);
            Session.SaveChangesAsync();
            return "true";
        }
        

    }
}
