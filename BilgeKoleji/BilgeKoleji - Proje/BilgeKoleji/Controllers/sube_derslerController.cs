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
    public class sube_derslerController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: sube_dersler
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                var subeDersler = db.subeDersler.Where(x => x.silindiMi == false && x.sube.silindiMi==false && x.ders.silindiMi==false).Include(s => s.ders).Include(s => s.sube);
                return View(subeDersler.ToList());
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

           
        }

        // GET: sube_dersler/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                sube_ders sube_ders = db.subeDersler.Find(id);
                if (sube_ders == null)
                {
                    return HttpNotFound();
                }
                return View(sube_ders);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: sube_dersler/Create
        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {

                ViewBag.ders_id = new SelectList(db.dersler.Where(sd => sd.silindiMi ==false), "id", "isim");
                ViewBag.sube_id = new SelectList(db.subeler.Where( s => s.silindiMi == false), "id", "isim");
                return View();
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: sube_dersler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,sube_id,ders_id,silindiMi")] sube_ders sube_ders)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    sube_ders.donem_id = db.donemler.FirstOrDefault(d => d.baslangicTarihi <= DateTime.Now && d.bitisTarihi > DateTime.Now).id;
                    db.subeDersler.Add(sube_ders);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ders_id = new SelectList(db.dersler, "id", "isim", sube_ders.ders_id);
                ViewBag.sube_id = new SelectList(db.subeler, "id", "isim", sube_ders.sube_id);
                return View(sube_ders);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: sube_dersler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                sube_ders sube_ders = db.subeDersler.Find(id);
                if (sube_ders == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ders_id = new SelectList(db.dersler.Where(d => d.silindiMi ==false ), "id", "isim", sube_ders.ders_id);
                ViewBag.sube_id = new SelectList(db.subeler.Where(s => s.silindiMi == false), "id", "isim", sube_ders.sube_id);
                return View(sube_ders);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: sube_dersler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,sube_id,ders_id,silindiMi")] sube_ders sube_ders)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {

                if (ModelState.IsValid)
                {
                    db.Entry(sube_ders).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ders_id = new SelectList(db.dersler, "id", "isim", sube_ders.ders_id);
                ViewBag.sube_id = new SelectList(db.subeler, "id", "isim", sube_ders.sube_id);
                return View(sube_ders);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: sube_dersler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                sube_ders sube_ders = db.subeDersler.Find(id);
                if (sube_ders == null)
                {
                    return HttpNotFound();
                }
                sube_ders.silindiMi = true;
                db.Entry(sube_ders).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "sube_dersler");
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
