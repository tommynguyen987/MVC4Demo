﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMusicStore.Models;
using PagedList;

namespace MvcMusicStore.Controllers
{
    public class StoreManagerController : BaseController
    {
        private MusicStoreDBContext db = new MusicStoreDBContext();

        //
        // GET: /StoreManager/

        public ActionResult Index(int ? page)
        {            
            var listPaged = GetPagedNames(string.Empty, page); // GetPagedNames is found in BaseController
            if (listPaged == null)
                return HttpNotFound();
            
            return View(listPaged);
        }

        //
        // GET: /StoreManager/Details/5

        public ActionResult Details(int id = 0)
        {
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // GET: /StoreManager/CreateGenre

        public ActionResult CreateGenre()
        {
            return View();
        }

        //
        // GET: /StoreManager/CreateArtist

        public ActionResult CreateArtist()
        {
            return View();
        }

        //
        // GET: /StoreManager/Create

        public ActionResult Create()
        {
            ViewBag.Genres = new SelectList(db.Genres.OrderBy(g=>g.Name), "GenreId", "Name");
            ViewBag.Artists = new SelectList(db.Artists.OrderBy(g => g.Name), "ArtistId", "Name");
            return View();
        }

        //
        // POST: /StoreManager/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        //
        // POST: /StoreManager/CreateGenre

        [HttpPost]
        public ActionResult CreateGenre(Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(genre);
        }

        //
        // POST: /StoreManager/CreateArtist

        [HttpPost]
        public ActionResult CreateArtist(Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artist);
        }

        //
        // GET: /StoreManager/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        //
        // POST: /StoreManager/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
                ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
                return View(album);
            }
        }

        [HttpPost]
        public ActionResult Edit1()
        {
            var album = new Album();
            album.GenreId = int.Parse(Request.Form["GenreId"]);
            album.ArtistId = int.Parse(Request.Form["ArtistId"]);
            album.Title = Request.Form["Title"];
            album.Price = decimal.Parse(Request.Form["Price"]);
            TryUpdateModel(album);
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
                ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
                return View(album);
            }
        }

        //
        // GET: /StoreManager/Delete/5

        public ActionResult Delete(int id = 0)
        {
            //Album album = db.Albums.Find(id);
            //if (album == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(album);
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /StoreManager/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}