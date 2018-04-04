﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DraftCraft.DAL;
using DraftCraft.Models;

namespace DraftCraft.Controllers
{
    public class KategorijeController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Kategorije
        public ActionResult Index()
        {

            return View(db.Kategorije.ToList());
        }

        // GET: Kategorije/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorija kategorija = db.Kategorije.Find(id);
            if (kategorija == null)
            {
                return HttpNotFound();
            }
            return View(kategorija);
        }

        // GET: Kategorije/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kategorije/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Naziv")] Kategorija kategorija)
        {
            if (ModelState.IsValid)
            {
                db.Kategorije.Add(kategorija);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kategorija);
        }

        // GET: Kategorije/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorija kategorija = db.Kategorije.Find(id);
            if (kategorija == null)
            {
                return HttpNotFound();
            }
            return View(kategorija);
        }

        // POST: Kategorije/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Naziv")] Kategorija kategorija)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategorija).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategorija);
        }

        // GET: Kategorije/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorija kategorija = db.Kategorije.Find(id);
            if (kategorija == null)
            {
                return HttpNotFound();
            }
            return View(kategorija);
        }

        // POST: Kategorije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            Kategorija kategorija = db.Kategorije.Find(id);

            foreach (var p in kategorija.Proizvodi)
            {
                p.KategorijaID = null;
            }

            db.Kategorije.Remove(kategorija);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
