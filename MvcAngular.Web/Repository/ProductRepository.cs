using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcAngular.Web.Models;
using Raven.Client;
using Raven.Client.Document;
using System.Web.Http;

namespace MvcAngular.Web.Repository
{
    public class ProductRepository
    {
        private static readonly IDocumentStore documentStore;
        public static IDocumentSession _session;

        static ProductRepository()
        {
            documentStore = new DocumentStore
                                {
                                    //Url = "https://diver.ravenhq.com/databases/tmoodley-TriangleDB",
                                    //ApiKey = "dc482aab-f87e-4cd6-810c-c5157cae0e02"
                                    Url = "http://localhost:8080/databases/triangle" 
                                }.Initialize();

            _session = documentStore.OpenSession();
        }

        public IEnumerable<Product> GetSomeProduct()
        {
            List<Product> products = (from product in _session.Query<Product>()
                                      select product).Take(30).ToList();

            return products;
           
        }

        public ProductResponse GetProduct(ProductRequest request)
        {
            request.Validate();
            _session = documentStore.OpenSession();
                List<Product> query = (from product in _session.Query<Product>()
                                          select product).ToList();

                var results =
                    query
                        .Skip((request.PageIndex - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .GroupBy(r => new { Total = query.Count() })
                        .ToList();

                if (results.Count == 0)
                {
                    return
                        new ProductResponse
                        {
                            Total = 0,
                            Page = 0,
                            Records = 0,
                            Rows = Enumerable.Empty<Product>().ToList()
                        };
                }

                int totalRecordCount = results[0].Key.Total;
                return new ProductResponse
                    {
                        Total = totalRecordCount / request.PageSize,
                        Page = request.PageIndex,
                        Records = totalRecordCount,
                        Rows = results[0].ToList()
                    };
            }
        

        public Product ReadProduct(int id)
        {
            Product product = _session.Load<Product>(id);
            return product;
        }

        public void CreateProduct(Product p)
        {  
            _session.Store(p);
            _session.SaveChanges(); 
        }

        public void UpdateProduct(Product p)
        {
            
            Product product = _session.Load<Product>(Convert.ToInt32(p.Id)); 
            product.Name = p.Name; 
            product.CategoryId = p.CategoryId;
            product.SupplierId = p.SupplierId;
            product.Code = p.Code;
            product.CreatedAt = p.CreatedAt;
            product.StandardCost = p.StandardCost;
            product.ListPrice = p.ListPrice;
            product.PhotoFile = p.PhotoFile;
            product.CreatedBy = p.CreatedBy;
            _session.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
             Product product = _session.Load<Product>(id);
       
            _session.Delete(product);
            _session.SaveChanges(); 
        }
    }
}