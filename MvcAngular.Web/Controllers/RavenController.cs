using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcAngular.Web.Models;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Document;

namespace MvcAngular.Web.Repository
{
    public class RavenController : Controller
    {
    private static readonly IDocumentStore documentStore;
    private IDocumentSession _session;

    static RavenController()
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
         
    }
}