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
    public class onkayitlarController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: onkayitlar
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                var ogrenciler = db.ogrenciler.Where(o => o.onKayitMi == true && o.silindiMi == false).ToList();
                return View(ogrenciler.ToList());
            }
            else
            {
                return RedirectToAction("index", "giris");
            }


        }


        // GET: onkayitlar/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                onkayit onkayit = db.onKayitlar.Find(id);
                if (onkayit == null)
                {
                    return HttpNotFound();
                }
                return View(onkayit);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: onkayitlar/Create
        public ActionResult Create()
        {

            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                ViewBag.veli_id = new SelectList(db.veliler, "id", "adSoyad");
                ogrenci o = new ogrenci();
                o.devamDurumu = true;
                o.okulNo = okulNo();
                return View(o);
            }


            else
            {

                return RedirectToAction("index", "giris");
            }
        }

        public string okulNo()
        {
            string okulNo = "";
            List<string> bol = new List<string>();

            if (db.ogrenciler.Count() != 0)
            {

                okulNo = db.ogrenciler.Where(o => o.silindiMi == false && o.devamDurumu == true)
               .OrderByDescending(o => o.id)
               .First().okulNo;

                bol = okulNo.Split('-').ToList();

                if (bol[1] == DateTime.Now.Year.ToString().Substring(2, 2))
                {
                    okulNo = (Convert.ToInt32(bol[0]) + 1).ToString() + "-" + bol[1];
                }
                else
                {
                    okulNo = "100-" + DateTime.Now.Year.ToString().Substring(2, 2);
                }

            }
            else
            {
                okulNo = "100-" + DateTime.Now.Year.ToString().Substring(2, 2);
            }




            return okulNo;
        }

        // POST: onkayitlar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,adSoyad,okulNo,cinsiyet,bitirdigiOkul,NotOrtalamasi,veli_id,silindiMi")]ogrenci ogrenci)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {

                if (ModelState.IsValid)
                {
                    ogrenci.onKayitMi = true;
                    ogrenci.devamDurumu = true;
                    db.ogrenciler.Add(ogrenci);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.veli_id = new SelectList(db.veliler, "id", "adSoyad", ogrenci.veli_id);
                return View(ogrenci);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: onkayitlar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci ogrenci = db.ogrenciler.Find(id);
                if (ogrenci == null)
                {
                    return HttpNotFound();
                }
                ViewBag.veli_id = new SelectList(db.veliler, "id", "adSoyad", ogrenci.veli_id);
                return View(ogrenci);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: onkayitlar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,adSoyad,okulNo,cinsiyet,bitirdigiOkul,NotOrtalamasi,onKayitMi,devamDurumu,veli_id,silindiMi")] ogrenci ogrenci)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {

                if (ModelState.IsValid)
                {
                    db.Entry(ogrenci).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.veli_id = new SelectList(db.veliler, "id", "adSoyad", ogrenci.veli_id);
                return View(ogrenci);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }


        public ActionResult tasi(int id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                ogrenci ogrenci = db.ogrenciler.Find(id);
                if (ModelState.IsValid)
                {

                   
                    ogrenci.onKayitMi = false;               
                    db.Entry(ogrenci).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index", "onKayitlar");
                }
                ViewBag.veli_id = new SelectList(db.veliler, "id", "adSoyad", ogrenci.veli_id);
                return RedirectToAction("Index");
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: onkayitlar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci ogrenci = db.ogrenciler.Find(id);
                if (ogrenci == null)
                {
                    return HttpNotFound();
                }

                ogrenci.silindiMi = true;
                db.Entry(ogrenci).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "onkayitlar");
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
