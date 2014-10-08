using MvcMusicStore.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMusicStore.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IPagedList<Album> GetPagedNames(string searchString, int? page)
        {
            // return a 404 if user browses to before the first page
            if (page.HasValue && page < 1)
                return null;

            // retrieve list from database/whereverand
            var listUnpaged = GetAlbums(searchString);
                        
            // page the list
            const int pageSize = 3;
            var listPaged = listUnpaged.ToPagedList(page ?? 1, pageSize);

            // return a 404 if user browses to pages beyond last page. special case first page if no items exist
            if (listPaged.PageNumber != 1 && page.HasValue && page > listPaged.PageCount)
                return null;

            return listPaged;
        }

        private List<Album> GetAlbums(string searchString)
        {
            var storeDB = new MusicStoreDBContext();
            if (!string.IsNullOrEmpty(searchString))
            {
                return storeDB.Albums.Include("Artist").Where(a => a.Title.Contains(searchString)).ToList();
            }
            return storeDB.Albums.Include("Artist").ToList();;
        }
    }
}
