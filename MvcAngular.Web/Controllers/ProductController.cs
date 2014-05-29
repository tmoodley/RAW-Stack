using MvcAngular.Web.API;
using MvcAngular.Web.Models;
using MvcAngular.Web.Repository;
using Raven.Abstractions;
using Raven.Client;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAngular.Web.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }

    private static readonly IDocumentStore documentStore;
    private IDocumentSession _session;

    static ProductController()
    {
        documentStore = new DocumentStore
                            {
                                //Url = "https://diver.ravenhq.com/databases/tmoodley-TriangleDB",
                                //ApiKey = "dc482aab-f87e-4cd6-810c-c5157cae0e02"
                                Url = "http://localhost:8080/databases/triangle" 
                            }.Initialize();
         
    }

    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        _session = documentStore.OpenSession();
    }

    protected override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        _session.SaveChanges();
        _session.Dispose();
    }
         
    public ActionResult StoreSomeProductInDatabase()
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
        _session.Store(product);
        _session.SaveChanges();

        return Content(product.Id);
    }

    public ActionResult InsertSomeMoreProducts()
    {
        for (int i = 0; i < 50; i++)
        {
            var product = new Product
            {
                Name = "Product Name " + i,
                CategoryId = "category/1024",
                SupplierId = "supplier/16",
                Code = "H11050" + i,
                CreatedAt = DateTime.Now,
                StandardCost = 250 + (i * 10),
                ListPrice = 189 + (i * 10),
            };
            _session.Store(product);
        }

        _session.SaveChanges();

        return Content("Products successfully created");
    }
    [HttpGet]
    public ActionResult GetProduct(int id)
    {
        Product product = _session.Load<Product>(id);
        ViewBag.product = product;
        return View(product);
    }
    [HttpPost]
    public ActionResult GetProduct(Product p)
    {
        var repo = new ProductRepository();
        repo.UpdateProduct(p);
        return Content("Product " + p.Id + " successfully updated");
    }

    public ActionResult GetProducts()
    {
        List<Product> products = (from product in _session.Query<Product>()
                                  where product.CategoryId.Equals("category/1033")
                                  select product).ToList();

         return View(products.ToList());
    }
            



    }
}
