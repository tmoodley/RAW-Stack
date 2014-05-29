using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using MvcAngular.Web.Models;
using MvcAngular.Web.Models.Binders;
using MvcAngular.Web.Repository;
using Raven.Client.Document;
using Raven.Client;
using System.Web.Http.Controllers;
using System.Threading;
using System.Threading.Tasks;

namespace MvcAngular.Web.API
{
    public class ProductController : ApiController
    {
        public ProductResponse Get([ModelBinder] ProductRequest model)
        {
            model = model ?? new ProductRequest();
            var repository = new ProductRepository();
            return repository.GetProduct(model);
        }

        public Product Get(int id)
        {
            var repository = new ProductRepository();
            var product = repository.ReadProduct(id);
            if (product == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return product;
        }

        public void Post(Product product)
        {
            var repository = new ProductRepository();
            repository.CreateProduct(product);
        }

        public void Put(Product product)
        {
            var repository = new ProductRepository();
            repository.UpdateProduct(product);
        }

        public void Delete(int id)
        {
            var repository = new ProductRepository();
            repository.DeleteProduct(id);
        }
    }
}
