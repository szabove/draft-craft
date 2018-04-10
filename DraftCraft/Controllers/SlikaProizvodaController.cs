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
using System.Web.Helpers;
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;

namespace DraftCraft.Controllers
{
    public class SlikaProizvodaController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: SlikaProizvoda
        public ActionResult Index()
        {
            return View(db.SlikeProizvoda.ToList());
        }

        // GET: SlikaProizvoda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlikaProizvoda slikaProizvoda = db.SlikeProizvoda.Find(id);
            if (slikaProizvoda == null)
            {
                return HttpNotFound();
            }
            return View(slikaProizvoda);
        }

        // GET: SlikaProizvoda/Create
        public ActionResult Upload()
        {
            return View();
        }

        // POST: SlikaProizvoda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase[] files)
        {
            //if (file != null)
            //{
            //    if (ValidateFile(file))
            //    {
            //        try
            //        {
            //            SaveFileToDisk(file);
            //        }
            //        catch (Exception)
            //        {

            //            ModelState.AddModelError("ImeDatoteke", "Došlo je do pogreške, molimo pokušajte ponovo!");
            //        }
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("ImeDatoteke", "Datoteka mora biti u jednom od formata: gif, png, jpeg ili jpg i manja od 2MB");
            //    }
            //}
            //else
            //{
            //    //if the user has not entered a file return an error message
            //    ModelState.AddModelError("ImeDatoteke", "Odaberite datoteku");
            //}

            //if (ModelState.IsValid)
            //{
            //    db.SlikeProizvoda.Add(new SlikaProizvoda { ImeDatoteke = file.FileName});
            //   try
            //        {
            //            db.SaveChanges();
            //        }
            //    catch (DbUpdateException ex)
            //        {
            //            SqlException innerException = ex.InnerException.InnerException as SqlException;
            //            if (innerException != null && innerException.Number == 2601)
            //            {
            //                ModelState.AddModelError("ImeDatoteke", "Datoteka " + file.FileName +
            //                " već postoji.Pokušte obrisati postojeću datoteku ili trenutnoj promijenite naziv!");
            //            }
            //            else
            //            {
            //                ModelState.AddModelError("ImeDatoteke", "Sorry an error has occurred saving to the database, please try again");
            //            }
            //                return View();
            //            }
            //            return RedirectToAction("Index");
            //    }
            bool allValid = true;
            string invalidFiles = "";

            //chek the user has entered a file
            if (files[0] != null)
            {
                // if the user has entered less than ten files
                if (files.Length <= 10)
                {
                    //check they are all valid
                    foreach (var file in files)
                    {
                        if (!ValidateFile(file))
                        {
                            allValid = false;
                            invalidFiles += ", " + file.FileName;
                        }
                    }
                    //if they are all valid then try to save them to disk
                    if (allValid)
                    {
                        foreach (var file in files)
                        {
                            try
                            {
                                SaveFileToDisk(file);
                            }
                            catch (Exception)
                            {

                                ModelState.AddModelError("ImeDatoteke", "Pogreška! Pokušajte ponovno!");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("ImeDatoteke", "Sve datoteke moraju biti formata: gif, png, jpeg ili jpg i manje od 2MB. Sljedeće datoteke nisu prihvaćene: " + invalidFiles + "! ");
                    }
                }
                //the user has entered more than 10 files
                else
                {
                    ModelState.AddModelError("ImeDatoteke", "Molimn odaberite do 10 datoteka istovremeno!");
                }
            }
            else
            {
                //if the user not entered a file
                ModelState.AddModelError("ImeDatoteke", "Molim odaberite datoteku!");
            }

            if (ModelState.IsValid)
            {
                bool duplicates = false;
                bool otherDbError = false;
                string duplicateFiles = "";

                foreach (var file in files)
                {
                    //try and save each file
                    var productToAdd = new SlikaProizvoda { ImeDatoteke = file.FileName };
                    try
                    {
                        db.SlikeProizvoda.Add(productToAdd);
                        db.SaveChanges();
                    }
                    //if there is an exception chek if it is caused by a duplicate file
                    catch (DbUpdateException ex)
                    {
                        SqlException innerException = ex.InnerException.InnerException as SqlException;
                        if (innerException != null && innerException.Number == 2601)
                        {
                            duplicateFiles += ", " + file.FileName;
                            duplicates = true;
                            db.Entry(productToAdd).State = EntityState.Detached;
                        }
                        else
                        {
                            otherDbError = true;
                        }
                    }
                }
                //add a list of duplicate files to the error message
                if (duplicates)
                {
                    ModelState.AddModelError("ImeDatoteke", "Sve datoteke su uspješno učitane osim: " + duplicateFiles + ", već postoje! ");
                    return View();
                }
                else if (otherDbError)
                {
                    ModelState.AddModelError("ImeDatoteke", "Došlo je do pogreške prilikom zapisa u bazu podataka. Molim pokuštaje ponovo!");
                    return View();
                }

                return RedirectToAction("Index");
               
            }

            return View();
        }

        // GET: SlikaProizvoda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlikaProizvoda slikaProizvoda = db.SlikeProizvoda.Find(id);
            if (slikaProizvoda == null)
            {
                return HttpNotFound();
            }
            return View(slikaProizvoda);
        }

        // POST: SlikaProizvoda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ImeDatoteke")] SlikaProizvoda slikaProizvoda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slikaProizvoda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slikaProizvoda);
        }

        // GET: SlikaProizvoda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlikaProizvoda slikaProizvoda = db.SlikeProizvoda.Find(id);
            if (slikaProizvoda == null)
            {
                return HttpNotFound();
            }
            return View(slikaProizvoda);
        }

        // POST: SlikaProizvoda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SlikaProizvoda slikaProizvoda = db.SlikeProizvoda.Find(id);
            db.SlikeProizvoda.Remove(slikaProizvoda);
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
        //Provjera validnosti upload-abe datoteke
        private bool ValidateFile(HttpPostedFileBase file)
        {
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            string[] allowedFileTypes = { ".gif", ".png", ".jpeg", ".jpg" };
            if (file.ContentLength >0 && file.ContentLength < 2097152 && allowedFileTypes.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }

        //Resizing the image and thumbnails
        private void SaveFileToDisk(HttpPostedFileBase file)
        {
            WebImage img = new WebImage(file.InputStream);
            if (img.Width>190)
            {
                img.Resize(190, img.Height);
            }
            img.Save(Constants.SlikeProizvodaPath + file.FileName);

            if (img.Width > 100)
            {
                img.Resize(100, img.Height);
            }
            img.Save(Constants.ThumbnailsProizvodaPath + file.FileName);
        }


    }
}
