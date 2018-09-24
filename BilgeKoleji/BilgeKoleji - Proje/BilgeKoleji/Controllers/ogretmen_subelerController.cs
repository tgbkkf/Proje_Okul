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
    public class ogretmen_subelerController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: ogretmen_subeler
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                var ogretmen_sube = db.ogretmen_sube.Include(o => o.ogretmen).Include(o => o.sube);
                return View(ogretmen_sube.Where(x => x.silindiMi == false && x.ogretmen.silindiMi==false && x.sube.silindiMi==false).ToList());
            }
            else
            {
                return RedirectToAction("index", "giris");
            }
          
        }

        public ActionResult IndexOgretmen()
        {


            if (Convert.ToInt32(Session["kulTip"]) == 2 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                int kullanici = Convert.ToInt32(db.kullanicilar.Find(Convert.ToInt32(Session["kulId"])).ogretmen_id);
                var ogretmen_subeler = db.ogretmen_sube.Where(x => x.ogretmen_id == kullanici && x.silindiMi == false && x.sube.silindiMi != true).ToList();
                return View(ogretmen_subeler);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }
        }

      
        // GET: ogretmen_subeler/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogretmen_sube ogretmen_sube = db.ogretmen_sube.Find(id);
                if (ogretmen_sube == null)
                {
                    return HttpNotFound();
                }
                return View(ogretmen_sube);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // GET: ogretmen_subeler/Create
        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                ViewBag.ogretmen_id = new SelectList(db.ogretmen.Where(o => o.silindiMi == false), "id", "adiSoyadi");
                ViewBag.sube_id = new SelectList(db.subeler.Where(o => o.silindiMi == false), "id", "isim");
                return View();
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: ogretmen_subeler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ogretmen_id,sube_id,silindiMi")] ogretmen_sube ogretmen_sube)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    ogretmen_sube.donem_id = db.donemler.FirstOrDefault(d => d.baslangicTarihi <= DateTime.Now && d.bitisTarihi > DateTime.Now).id;
                    db.ogretmen_sube.Add(ogretmen_sube);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ogretmen_id = new SelectList(db.ogretmen, "id", "adiSoyadi", ogretmen_sube.ogretmen_id);
                ViewBag.sube_id = new SelectList(db.subeler, "id", "isim", ogretmen_sube.sube_id);
                return View(ogretmen_sube);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogretmen_subeler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogretmen_sube ogretmen_sube = db.ogretmen_sube.Find(id);
                if (ogretmen_sube == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ogretmen_id = new SelectList(db.ogretmen.Where(o => o.silindiMi == false), "id", "adiSoyadi", ogretmen_sube.ogretmen_id);
                ViewBag.sube_id = new SelectList(db.subeler.Where(o => o.silindiMi == false), "id", "isim", ogretmen_sube.sube_id);
                return View(ogretmen_sube);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: ogretmen_subeler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ogretmen_id,donem_id,sube_id,silindiMi")] ogretmen_sube ogretmen_sube)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(ogretmen_sube).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ogretmen_id = new SelectList(db.ogretmen, "id", "adiSoyadi", ogretmen_sube.ogretmen_id);
                ViewBag.sube_id = new SelectList(db.subeler, "id", "isim", ogretmen_sube.sube_id);
                return View(ogretmen_sube);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogretmen_subeler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogretmen_sube ogretmen_sube = db.ogretmen_sube.Find(id);
                if (ogretmen_sube == null)
                {
                    return HttpNotFound();
                }
                ogretmen_sube.silindiMi = true;
                db.Entry(ogretmen_sube).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "ogretmen_subeler");
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
