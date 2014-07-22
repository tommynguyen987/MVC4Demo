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
                                  .Where(a => a.Title.Contains(q))
                                  .Take(10);

            return View(albums);
        }

        public ActionResult QuickSearch(string term)
        {
            var artists = GetArtists(term).Select(a => new { value = a.Name });
            return Json(artists, JsonRequestBehavior.AllowGet);
        }

        private List<Artist> GetArtists(string searchString)
        {
            var storeDB = new MusicStoreDBContext();
            return storeDB.Artists
                    .Where(a => a.Name.Contains(searchString))
                    .ToList();
        }

    }
}
