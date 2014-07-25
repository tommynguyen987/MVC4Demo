using MvcMusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Hello world !!!";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Search(string q)
        {
            var db = new MusicStoreDBContext();
            var albums = db.Albums.Include("Artist")
                                  .Where(a => a.Title.Contains(q));
                                  //.Take(10);

            return View(albums);
        }

        public ActionResult QuickSearch(string term)
        {
            var albums = GetAlbums(term).Select(a => new { value = a.Title });
            return Json(albums, JsonRequestBehavior.AllowGet);
        }

        private List<Album> GetAlbums(string searchString)
        {
            var storeDB = new MusicStoreDBContext();
            return storeDB.Albums.Include("Artist")
                    .Where(a => a.Title.Contains(searchString))
                    .ToList();                    
        }

        public ActionResult AlbumSearch(string q)
        {
            var albums = GetAlbums(q);
            return Json(albums, JsonRequestBehavior.AllowGet);
        }

    }
}
