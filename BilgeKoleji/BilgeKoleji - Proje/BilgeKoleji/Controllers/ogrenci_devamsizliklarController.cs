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
    public class ogrenci_devamsizliklarController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: ogrenci_devamsizliklar
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && Session["kulId"] != null || Convert.ToInt32(Session["kulTip"]) == 2 && Session["kulId"] != null)
            {
                if (Convert.ToInt32(Session["kulTip"]) == 1)
                {
                    var ogrenci_devamsizliklar = db.ogrenciDevamsizliklar.Where(o => o.silindiMi == false && o.ogrenci.devamDurumu==true && o.ogrenci.onKayitMi == false).ToList();
                    return View(ogrenci_devamsizliklar);
                }
                else
                {
                    int ogretmenId = (Convert.ToInt32(Session["kulId"]));
                    List<ogrenci_devamsizlik> odList = new List<ogrenci_devamsizlik>();
                    List<ogrenci> oList = new List<ogrenci>();
                    List<sube> sList = new List<sube>();
                    sList = db.ogretmen.Find(db.kullanicilar.Find(ogretmenId).ogretmen_id).ogretmenSubeler.Where(os => os.silindiMi == false && os.ogretmen.silindiMi == false && os.sube.silindiMi == false).Select(os => os.sube).ToList();


                    foreach (var item in sList)
                    {
                        foreach (var itemOgrenci in item.ogrenciSubeler.Select(os => os.ogrenci))
                        {
                            foreach (var itemOgrenciDevamsizlik in itemOgrenci.ogrenciDevamsizliklar)
                            {
                                ogrenci_devamsizlik od = new ogrenci_devamsizlik();
                                od = itemOgrenciDevamsizlik;
                                odList.Add(od);
                            }
                        }
                    }
                    
                    return View(odList.Where(o => o.silindiMi == false && o.ogrenci.devamDurumu == true && o.ogrenci.onKayitMi == false));
                }

            }
            else
            {
                return RedirectToAction("index", "giris");

            }

        }


        public ActionResult IndexOgrenci()
        {


            if (Convert.ToInt32(Session["kulTip"]) == 3 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {

                int kullanici = Convert.ToInt32(db.kullanicilar.Find(Convert.ToInt32(Session["kulId"])).ogrenci_id);
                var ogrenci_devamsizliklar = db.ogrenciDevamsizliklar.Where(x => x.ogrenci_id == kullanici && x.silindiMi==false).ToList();
                return View(ogrenci_devamsizliklar);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogrenci_devamsizliklar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ogrenci_devamsizlik ogrenci_devamsizlik = db.ogrenciDevamsizliklar.Find(id);
            if (ogrenci_devamsizlik == null)
            {
                return HttpNotFound();
            }
            return View(ogrenci_devamsizlik);
        }


        public ActionResult OgretmenCreate()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 2 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {

                ViewBag.devamsizlik_id = new SelectList(db.devamsizliklar.Where(d => d.silindiMi == false), "id", "name");

                List<ogrenci> oList = new List<ogrenci>();
                List<sube> sList = new List<sube>();
                sList = db.ogretmen.Find(db.kullanicilar.Find(Convert.ToInt32(Session["kulId"])).ogretmen_id).ogretmenSubeler.Select(os => os.sube).ToList();

                foreach (var item in sList)
                {
                    foreach (var itemOgrenci in item.ogrenciSubeler.Select(os => os.ogrenci))
                    {
                        ogrenci o = new ogrenci();

                        o = itemOgrenci;
                        oList.Add(o);
                    }
                }
                ViewBag.ogrenci_id = new SelectList(oList, "id", "adSoyad");
                return View();
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

           
        }
        // POST: ogrenci_devamsizliklar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OgretmenCreate([Bind(Include = "id,devamsizlik_id,ogrenci_id,tarih,silindiMi")] ogrenci_devamsizlik ogrenci_devamsizlik)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 2 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    ogrenci_devamsizlik.donem_id = db.donemler.FirstOrDefault(d => d.baslangicTarihi <= DateTime.Now && d.bitisTarihi > DateTime.Now).id;
                    db.ogrenciDevamsizliklar.Add(ogrenci_devamsizlik);
                    db.SaveChanges();
                    return RedirectToAction("Index","ogrenci_devamsizliklar");
                }

                ViewBag.devamsizlik_id = new SelectList(db.devamsizliklar, "id", "id", ogrenci_devamsizlik.devamsizlik_id);
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad", ogrenci_devamsizlik.ogrenci_id);
                return View(ogrenci_devamsizlik);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // GET: ogrenci_devamsizliklar/Create
        public ActionResult Create()
        {

            if (Convert.ToInt32(Session["kulTip"]) == 2 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                return RedirectToAction("OgretmenCreate","ogrenci_devamsizliklar");
            }

            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                ViewBag.devamsizlik_id = new SelectList(db.devamsizliklar.Where(d => d.silindiMi == false), "id", "name");
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler.Where(o => o.silindiMi == false && o.devamDurumu == true), "id", "adSoyad");
                return View();
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }


        // POST: ogrenci_devamsizliklar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,devamsizlik_id,ogrenci_id,tarih,silindiMi")] ogrenci_devamsizlik ogrenci_devamsizlik)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    ogrenci_devamsizlik.donem_id = db.donemler.FirstOrDefault(d => d.baslangicTarihi <= DateTime.Now && d.bitisTarihi > DateTime.Now).id;
                    db.ogrenciDevamsizliklar.Add(ogrenci_devamsizlik);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.devamsizlik_id = new SelectList(db.devamsizliklar, "id", "name", ogrenci_devamsizlik.devamsizlik_id);
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad", ogrenci_devamsizlik.ogrenci_id);
                return View(ogrenci_devamsizlik);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogrenci_devamsizliklar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && Session["kulId"] != null || Convert.ToInt32(Session["kulTip"]) == 2 && Session["kulId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci_devamsizlik ogrenci_devamsizlik = db.ogrenciDevamsizliklar.Find(id);
                if (ogrenci_devamsizlik == null)
                {
                    return HttpNotFound();
                }
                ViewBag.devamsizlik_id = new SelectList(db.devamsizliklar.Where( d => d.silindiMi == false), "id", "Name", ogrenci_devamsizlik.devamsizlik_id);
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler.Where(o => o.silindiMi == false && o.devamDurumu == true), "id", "adSoyad", ogrenci_devamsizlik.ogrenci_id);
                return View(ogrenci_devamsizlik);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: ogrenci_devamsizliklar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,devamsizlik_id,donem_id,ogrenci_id,tarih,silindiMi")] ogrenci_devamsizlik ogrenci_devamsizlik)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && Session["kulId"] != null || Convert.ToInt32(Session["kulTip"]) == 2 && Session["kulId"] != null)
            { 
                if (ModelState.IsValid)
                {
                    db.Entry(ogrenci_devamsizlik).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            ViewBag.devamsizlik_id = new SelectList(db.devamsizliklar, "id", "id", ogrenci_devamsizlik.devamsizlik_id);
            ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad", ogrenci_devamsizlik.ogrenci_id);
            return View(ogrenci_devamsizlik);
             }

                else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogrenci_devamsizliklar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && Session["kulId"] != null || Convert.ToInt32(Session["kulTip"]) == 2 && Session["kulId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci_devamsizlik ogrenci_devamsizlik = db.ogrenciDevamsizliklar.Find(id);
                if (ogrenci_devamsizlik == null)
                {
                    return HttpNotFound();
                }
                ogrenci_devamsizlik.silindiMi = true;
                db.Entry(ogrenci_devamsizlik).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "ogrenci_devamsizliklar");
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
