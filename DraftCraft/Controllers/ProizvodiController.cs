using System;
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
    public class ProizvodiController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Proizvodi
        public ActionResult Index(string kategorija, string search)
        {
            var proizvodi = db.Proizvodi.Include(p => p.Kategorija);

            if (!String.IsNullOrEmpty(kategorija))
            {
                proizvodi = proizvodi.Where(p => p.Kategorija.Naziv == kategorija);
            }

            if (!String.IsNullOrEmpty(search))
            {
                proizvodi = proizvodi.Where(p => p.Naziv.Contains(search) || p.Opis.Contains(search) || p.Kategorija.Naziv.Contains(search));
                ViewBag.Search = search;
            }

            
            var kategorije = proizvodi.OrderBy(p => p.Kategorija.Naziv).Select(p => p.Kategorija.Naziv).Distinct();

            if (!String.IsNullOrEmpty(kategorija))
            {
                proizvodi = proizvodi.Where(p => p.Kategorija.Naziv == kategorija);
            }

            ViewBag.Kategorija = new SelectList(kategorije);

            return View(proizvodi.ToList());
        }

        // GET: Proizvodi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvodi.Find(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            return View(proizvod);
        }

        // GET: Proizvodi/Create
        public ActionResult Create()
        {
            ViewBag.KategorijaID = new SelectList(db.Kategorije, "ID", "Naziv");
            return View();
        }

        // POST: Proizvodi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Naziv,Opis,Cijena,Sirina,Visina,Dubina,KategorijaID")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                db.Proizvodi.Add(proizvod);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategorijaID = new SelectList(db.Kategorije, "ID", "Naziv", proizvod.KategorijaID);
            return View(proizvod);
        }

        // GET: Proizvodi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvodi.Find(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategorijaID = new SelectList(db.Kategorije, "ID", "Naziv", proizvod.KategorijaID);
            return View(proizvod);
        }

        // POST: Proizvodi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Naziv,Opis,Cijena,Sirina,Visina,Dubina,KategorijaID")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proizvod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategorijaID = new SelectList(db.Kategorije, "ID", "Naziv", proizvod.KategorijaID);
            return View(proizvod);
        }

        // GET: Proizvodi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proizvod proizvod = db.Proizvodi.Find(id);
            if (proizvod == null)
            {
                return HttpNotFound();
            }
            return View(proizvod);
        }

        // POST: Proizvodi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proizvod proizvod = db.Proizvodi.Find(id);
            db.Proizvodi.Remove(proizvod);
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
