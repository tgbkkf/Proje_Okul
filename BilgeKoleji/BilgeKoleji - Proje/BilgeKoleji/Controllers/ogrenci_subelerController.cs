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
    public class ogrenci_subelerController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: ogrenci_subeler
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                int kullanici = Convert.ToInt32(db.kullanicilar.Find(Convert.ToInt32(Session["kulId"])).ogrenci_id);


                var ogrenciSubeler = db.ogrenciSubeler.Include(o => o.ogrenci).Include(o => o.sube);
                return View(ogrenciSubeler.Where(x => x.silindiMi == false && x.ogrenci.silindiMi==false && x.ogrenci.devamDurumu==true).ToList());
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        public ActionResult OgretmenIndex()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 2 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {

                List<ogrenci_sube> osList = new List<ogrenci_sube>();
                List<sube> sList = new List<sube>();
                sList = db.ogretmen.Find(db.kullanicilar.Find(Convert.ToInt32(Session["kulId"])).ogretmen_id).ogretmenSubeler.Where(os => os.silindiMi == false && os.ogretmen.silindiMi == false && os.sube.silindiMi == false).Select(os => os.sube).ToList();


                foreach (var item in sList.Where( s => s.silindiMi == false))
                {
                    foreach (var itemOgrenci in item.ogrenciSubeler.Where(os => os.silindiMi == false && os.ogrenci.silindiMi == false && os.sube.silindiMi == false))
                    {
                        ogrenci_sube os = new ogrenci_sube();

                        os = itemOgrenci;
                        osList.Add(os);
                    }
                }
                return View(osList);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

        }


        // GET: ogrenci_subeler/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci_sube ogrenci_sube = db.ogrenciSubeler.Find(id);
                if (ogrenci_sube == null)
                {
                    return HttpNotFound();
                }
                return View(ogrenci_sube);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        public ActionResult Uyari()
        {

            return View();
        }


        // GET: ogrenci_subeler/Create
        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
        
                //db.ogrenciler.Intersect(db.ogrenciSubeler.Select(o => o.ogrenci))
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler.Where(o => o.silindiMi == false && o.devamDurumu == true).Except(db.ogrenciSubeler.Select(o => o.ogrenci)), "id", "adSoyad");
                ViewBag.sube_id = new SelectList(db.subeler.Where(s => s.silindiMi == false), "id", "isim");
                return View();
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: ogrenci_subeler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ogrenci_id,sube_id,silindiMi")] ogrenci_sube ogrenci_sube)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    if (db.ogrenciSubeler.Where(os => os.sube_id == ogrenci_sube.sube_id && os.silindiMi == false).Count() < 25)
                    {

                        db.ogrenciSubeler.Add(ogrenci_sube);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        
                        return RedirectToAction("uyari", "ogrenci_subeler");
                    }



                }

                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad", ogrenci_sube.ogrenci_id);
                ViewBag.sube_id = new SelectList(db.subeler, "id", "isim", ogrenci_sube.sube_id);
                return View(ogrenci_sube);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }




        // GET: ogrenci_subeler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci_sube ogrenci_sube = db.ogrenciSubeler.Find(id);
                if (ogrenci_sube == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler.Where(o => o.silindiMi == false && o.devamDurumu == true ), "id", "adSoyad", ogrenci_sube.ogrenci_id);
                ViewBag.sube_id = new SelectList(db.subeler.Where(s => s.silindiMi == false ), "id", "isim", ogrenci_sube.sube_id);
                return View(ogrenci_sube);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: ogrenci_subeler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ogrenci_id,sube_id,silindiMi")] ogrenci_sube ogrenci_sube)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(ogrenci_sube).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad", ogrenci_sube.ogrenci_id);
                ViewBag.sube_id = new SelectList(db.subeler, "id", "isim", ogrenci_sube.sube_id);
                return View(ogrenci_sube);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogrenci_subeler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci_sube ogrenci_sube = db.ogrenciSubeler.Find(id);
                if (ogrenci_sube == null)
                {
                    return HttpNotFound();
                }
                ogrenci_sube.silindiMi = true;
                db.Entry(ogrenci_sube).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "ogrenci_subeler");
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
