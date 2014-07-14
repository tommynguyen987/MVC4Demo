using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MvcMusicStore.Models;

namespace MvcMusicStore
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public class MusicStoreDBInitializer : DropCreateDatabaseAlways<MusicStoreDBContext>
        {            
            protected override void Seed(MusicStoreDBContext context)
            {
                context.Artists.Add(new Artist { Name = "Michael Jackson" });
                context.Genres.Add(new Genre { Name = "Pop" });
                context.Albums.Add(new Album { Artist = new Artist { Name = "SNSD" },
                                               Genre = new Genre {  Name = "Korea" },
                                               Price = 99.9m,
                                               Title = "The Best Collection"
                                            });
                base.Seed(context);
            }
        }
        protected void Application_Start()
        {
            //Database.SetInitializer(new MusicStoreDBInitializer());                       
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
    }
}