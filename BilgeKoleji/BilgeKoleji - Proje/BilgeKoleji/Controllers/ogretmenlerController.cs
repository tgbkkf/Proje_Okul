using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BilgeKoleji;
using BilgeKoleji.Models;

namespace BilgeKoleji.Controllers
{
    public class ogretmenlerController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: ogretmenler
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                return View(db.ogretmen.Where(x => x.silindiMi == false).ToList());
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

          
        }

      
        // GET: ogretmenler/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogretmen ogretmen = db.ogretmen.Find(id);
                if (ogretmen == null)
                {
                    return HttpNotFound();
                }
                return View(ogretmen);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogretmenler/Create
        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                return View();
            }

            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // POST: ogretmenler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,adiSoyadi,brans,cinsiyet,resim,gorev,telefon,cepTelefonu,ePosta,adres,silindiMi")] ogretmen ogretmen)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    db.ogretmen.Add(ogretmen);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(ogretmen);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogretmenler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogretmen ogretmen = db.ogretmen.Find(id);
                if (ogretmen == null)
                {
                    return HttpNotFound();
                }
                return View(ogretmen);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: ogretmenler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,adiSoyadi,brans,cinsiyet,resim,gorev,telefon,cepTelefonu,ePosta,adres,silindiMi")] ogretmen ogretmen)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(ogretmen).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(ogretmen);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogretmenler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogretmen ogretmen = db.ogretmen.Find(id);
                if (ogretmen == null)
                {
                    return HttpNotFound();
                }
                ogretmen.silindiMi = true;
                db.Entry(ogretmen).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "ogretmenler");
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
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
