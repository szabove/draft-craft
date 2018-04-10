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
using DraftCraft.ViewModels;
using PagedList;

namespace DraftCraft.Controllers
{
    public class ProizvodiController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Proizvodi
        public ActionResult Index(string kategorija, string search, string sortBy, int? page, int pageItems = 10)
        {
            //instantiate a new view model
            ProizvodIndexViewModel viewModel = new ProizvodIndexViewModel();

            var proizvodi = db.Proizvodi.Include(p => p.Kategorija);

           //Searching products by name || description || category
            if (!String.IsNullOrEmpty(search))
            {
                proizvodi = proizvodi.Where(p => p.Naziv.Contains(search) || p.Opis.Contains(search) || p.Kategorija.Naziv.Contains(search));
                viewModel.Search = search;
            }

            viewModel.KatSbrojacem = from matchingProducts in proizvodi
                                      where
                                      matchingProducts.KategorijaID != null
                                      group matchingProducts by
                                      matchingProducts.Kategorija.Naziv into
                                      catGroup
                                      select new KategorijeIbrojac()
                                      {
                                          NazivKategorije = catGroup.Key,
                                          ProizvodiCount = catGroup.Count()
                                      };

            if (!String.IsNullOrEmpty(kategorija))
            {
                proizvodi = proizvodi.Where(p => p.Kategorija.Naziv == kategorija);
                viewModel.Kategorija = kategorija;
            }

           // var kategorije = proizvodi.OrderBy(p => p.Kategorija.Naziv).Select(p => p.Kategorija.Naziv).Distinct();

            if (!String.IsNullOrEmpty(kategorija))
            {
                proizvodi = proizvodi.Where(p => p.Kategorija.Naziv == kategorija);
            }

            //sort the results
            switch (sortBy)
            {
                case "cijena_min":
                    proizvodi = proizvodi.OrderBy(p => p.Cijena);
                    break;
                case "cijena_max":
                    proizvodi = proizvodi.OrderByDescending(p => p.Cijena);
                    break;
                case "abc_min":
                    proizvodi = proizvodi.OrderBy(p => p.Naziv);
                    break;
                case "abc_max":
                    proizvodi = proizvodi.OrderByDescending(p => p.Naziv);
                    break;
                default:
                    proizvodi = proizvodi.OrderBy(p => p.Naziv);
                    break;
            }

            //Paging and items/page selection

            
            
            switch (pageItems)
            {
                case 1:
                    pageItems = 1;
                    break;
                case 5:
                    pageItems = 5;
                    break;
                case 10:
                    pageItems = 10;
                    break;
                case 15:
                    pageItems = 15;
                    break;
                case 20:
                    pageItems = 5;
                    break;
                default:
                    pageItems = 10;
                    break;
            }

            //const int PageItems = 3;
            //int PageItems;
            //bool isNum = int.TryParse(pageItems.ToString(), out PageItems);
            //if (isNum)
            //{
            //    PageItems = pageItems;
            //}
            //else
            //{
            //    PageItems = 10;
            //}

            viewModel.PageItems = pageItems;

            int currentPage = (page ?? 1);
            viewModel.Proizvodi = proizvodi.ToPagedList(currentPage, pageItems);
            viewModel.SortBy = sortBy;

            

            viewModel.PageItemNumber = new Dictionary<int, string> {
                {1, "1" },
                {5, "5" },
                {10, "10" },
                {15, "15" },
                {20, "20" }
            };



            //storing sort dictionary in Sorts variable
            viewModel.Sorts = new Dictionary<string, string>
            {
                {"- cijena +","cijena_min" },
                {"+ cijena -","cijena_max" },
                {"- abc +","abc_min" },
                {"+ abc -","abc_max" }
            };

            return View(viewModel);
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
