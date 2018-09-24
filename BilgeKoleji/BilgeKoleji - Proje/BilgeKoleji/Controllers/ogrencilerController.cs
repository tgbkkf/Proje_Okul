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
    public class ogrencilerController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: ogrenciler
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                var ogrenciler = db.ogrenciler.Where(o => o.silindiMi == false && o.devamDurumu == true && o.onKayitMi == false).Include(o => o.veli);

                return View(ogrenciler.ToList());
            }
            else
            {
                return RedirectToAction("index", "giris");
            }


           
        }

        // GET: ogrenciler/Details/5
        public ActionResult Details(int? id)
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
                return View(ogrenci);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogrenciler/Create
        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                ViewBag.veli_id = new SelectList(db.veliler, "id", "adSoyad");
                ogrenci o = new ogrenci();
                o.okulNo = okulNo();
                return View(o);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: ogrenciler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,adSoyad,okulNo,cinsiyet,veli_id,bitirdigiOkul,devamDurumu,silindiMi")] ogrenci ogrenci)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
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

        // GET: ogrenciler/Edit/5
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

        // POST: ogrenciler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,adSoyad,okulNo,cinsiyet,veli_id,bitirdigiOkul,devamDurumu,silindiMi")] ogrenci ogrenci)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(ogrenci).State = EntityState.Modified;

                    if (ogrenci.devamDurumu == false)
                    {
                        ogrenci.ogrenciNotlar = db.ogrenciNotlar.Where(on => on.ogrenci_id == ogrenci.id).ToList();

                        iliskisiKesilen ik = new iliskisiKesilen();
                        if (ogrenci.ogrenciNotlar.Count != 0)
                        {
                            ik.notOrtalamsi = ogrenci.ogrenciNotlar.Sum(on => on.not) / ogrenci.ogrenciNotlar.Count;
                        }
                        else
                        {
                            ik.notOrtalamsi = 0;
                        }
                        
                        ik.adSoyad = ogrenci.adSoyad;
                        ik.bitirdigiOkul = ogrenci.bitirdigiOkul;
                        ik.cinsiyet = ogrenci.cinsiyet;
                        ik.okulNo = ogrenci.okulNo;
                        ik.veli_id = ogrenci.veli_id;
                        db.Entry(ik).State = EntityState.Added;
                        db.SaveChanges();

                        ogrenci.silindiMi = true;
                        db.Entry(ogrenci).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");

                    }

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

      

        // GET: ogrenciler/Delete/5
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

                return RedirectToAction("index","ogrenciler");
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
