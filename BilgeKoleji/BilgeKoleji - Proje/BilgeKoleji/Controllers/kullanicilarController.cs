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
    public class kullanicilarController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: kullanicilar
        public ActionResult Index()
        {
            
                var kullanicilar = db.kullanicilar.Include(k => k.ogrenci).Include(k => k.ogretmen).Include(k => k.veli);
                return View(kullanicilar.Where(x => x.silindiMi == false).ToList());
            


        }

        // GET: kullanicilar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kullanici kullanici = db.kullanicilar.Find(id);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

        // GET: kullanicilar/Create
        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad");
                ViewBag.ogretmen_id = new SelectList(db.ogretmen, "id", "adiSoyadi");
                ViewBag.veli_id = new SelectList(db.veliler, "id", "adSoyad");
                return View();
            }

            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // POST: kullanicilar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,kulAdi,password,ogretmen_id,veli_id,ogrenci_id,silindiMi")] kullanici kullanici)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    db.kullanicilar.Add(kullanici);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad", kullanici.ogrenci_id);
                ViewBag.ogretmen_id = new SelectList(db.ogretmen, "id", "adiSoyadi", kullanici.ogretmen_id);
                ViewBag.veli_id = new SelectList(db.veliler, "id", "adSoyad", kullanici.veli_id);
                return View(kullanici);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // GET: kullanicilar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                kullanici kullanici = db.kullanicilar.Find(id);
                if (kullanici == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad", kullanici.ogrenci_id);
                ViewBag.ogretmen_id = new SelectList(db.ogretmen, "id", "adiSoyadi", kullanici.ogretmen_id);
                ViewBag.veli_id = new SelectList(db.veliler, "id", "adSoyad", kullanici.veli_id);
                return View(kullanici);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }


        }

        // POST: kullanicilar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,kulAdi,password,ogretmen_id,veli_id,ogrenci_id,silindiMi")] kullanici kullanici)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(kullanici).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad", kullanici.ogrenci_id);
                ViewBag.ogretmen_id = new SelectList(db.ogretmen, "id", "adiSoyadi", kullanici.ogretmen_id);
                ViewBag.veli_id = new SelectList(db.veliler, "id", "adSoyad", kullanici.veli_id);
                return View(kullanici);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // GET: kullanicilar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                kullanici kullanici = db.kullanicilar.Find(id);
                if (kullanici == null)
                {
                    return HttpNotFound();
                }
                kullanici.silindiMi = true;
                db.Entry(kullanici).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "kullanicilar");
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
