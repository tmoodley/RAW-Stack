using Raven.Client;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace MvcAngular.Web.API
{ 
        public abstract class RavenDbController : ApiController
        {
            public IDocumentStore Store
            {
                get { return LazyDocStore.Value; }
            }

            private static readonly Lazy<IDocumentStore> LazyDocStore = new Lazy<IDocumentStore>(() =>
            {

                var documentStore = new DocumentStore
                {
                    Url = "https://diver.ravenhq.com/databases/tmoodley-TriangleDB",
                    ApiKey = "dc482aab-f87e-4cd6-810c-c5157cae0e02"
                };
                documentStore.Initialize();

                return documentStore;
            });

            public IAsyncDocumentSession Session { get; set; }

            public async override Task<HttpResponseMessage> ExecuteAsync(
                HttpControllerContext controllerContext,
                CancellationToken cancellationToken)
            {
                using (Session = Store.OpenAsyncSession())
                {
                    var result = await base.ExecuteAsync(controllerContext, cancellationToken);
                    await Session.SaveChangesAsync();

                    return result;
                }
            }
        }
     
}
