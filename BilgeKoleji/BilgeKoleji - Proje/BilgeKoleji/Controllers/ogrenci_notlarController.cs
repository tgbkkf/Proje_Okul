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
    public class ogrenci_notlarController : Controller
    {
        private BilgeDb db = new BilgeDb();

        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && Session["kulId"] != null || Convert.ToInt32(Session["kulTip"]) == 2 && Session["kulId"] != null)
            {
                if (Convert.ToInt32(Session["kulTip"]) == 1)
                {
                    var ogrenciler = db.ogrenciler.Where(o => o.silindiMi == false && o.devamDurumu == true && o.onKayitMi==false).Include(o => o.veli);
                    return View(ogrenciler.ToList());
                }
                else
                {
                    int ogretmenId = (Convert.ToInt32(Session["kulId"]));
                    List<ogrenci_not> onList = new List<ogrenci_not>();
                    List<ogrenci> oList = new List<ogrenci>();
                    List<sube> sList = new List<sube>();
                    sList = db.ogretmen.Find(db.kullanicilar.Find(ogretmenId).ogretmen_id).ogretmenSubeler.Where(on => on.silindiMi == false && on.ogretmen.silindiMi == false && on.sube.silindiMi == false).Select(os => os.sube).ToList();


                    foreach (var item in sList)
                    {
                        foreach (var itemOgrenci in item.ogrenciSubeler.Where(os => os.silindiMi == false && os.ogrenci.silindiMi == false & os.sube.silindiMi == false).Where(os => os.silindiMi == false && os.ogrenci.silindiMi == false && os.sube.silindiMi== false).Select(os => os.ogrenci))
                        {
                            ogrenci o = new ogrenci();
                            o = itemOgrenci;
                            oList.Add(o);
                        }
                    }
                    
                    return View(oList);
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
                var ogrenci_notlar = db.ogrenciNotlar.Where(x => x.ogrenci_id == kullanici && x.silindiMi==false).ToList();
                return View(ogrenci_notlar);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

        }



        // GET: ogrenci_notlar
        public ActionResult OgrenciDetay(int id)
        {


            var ogrenciNotlar = db.ogrenciNotlar.Where(o => o.ogrenci_id == id && o.silindiMi == false).Include(o => o.ders).Include(o => o.ogrenci);
            return View(ogrenciNotlar.ToList());
            
        }

        // GET: ogrenci_notlar/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci_not ogrenci_not = db.ogrenciNotlar.Find(id);
                if (ogrenci_not == null)
                {
                    return HttpNotFound();
                }
                return View(ogrenci_not);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogrenci_notlar/Create
        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                ViewBag.ders_id = new SelectList(db.dersler.Where( d => d.silindiMi ==false), "id", "isim");
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler.Where(o => o.silindiMi == false && o.devamDurumu == true), "id", "adSoyad");
                return View();
            }else if (Convert.ToInt32(Session["kulTip"]) == 2 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                return RedirectToAction("OgretmenCreate", "ogrenci_notlar");
            }
            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: ogrenci_notlar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,not,ders_id,ogrenci_id,silindiMi")] ogrenci_not ogrenci_not)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    ogrenci_not.donem_id = db.donemler.FirstOrDefault(d => d.baslangicTarihi <= DateTime.Now && d.bitisTarihi > DateTime.Now).id;
                    db.ogrenciNotlar.Add(ogrenci_not);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


                ViewBag.ders_id = new SelectList(db.dersler, "id", "isim", ogrenci_not.ders_id);


                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad", ogrenci_not.ogrenci_id);
                return View(ogrenci_not);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        public ActionResult OgretmenCreate()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 2 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {

                ViewBag.ders_id = new SelectList(db.dersler, "id", "isim");

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
        public ActionResult OgretmenCreate([Bind(Include = "id,not,ders_id,ogrenci_id,silindiMi")] ogrenci_not ogrenci_not)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 2 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    ogrenci_not.donem_id = db.donemler.FirstOrDefault(d => d.baslangicTarihi <= DateTime.Now && d.bitisTarihi > DateTime.Now).id;
                    db.ogrenciNotlar.Add(ogrenci_not);
                    db.SaveChanges();
                    return RedirectToAction("Index", "ogrenci_notlar");
                }

                ViewBag.ogrenci_not = new SelectList(db.ogrenciNotlar, "id", "id", ogrenci_not.id);
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad", ogrenci_not.ogrenci_id);
                return View(ogrenci_not);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogrenci_notlar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && Session["kulId"] != null || Convert.ToInt32(Session["kulTip"]) == 2 && Session["kulId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci_not ogrenci_not = db.ogrenciNotlar.Find(id);
                if (ogrenci_not == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ders_id = new SelectList(db.dersler.Where(d => d.silindiMi == false), "id", "isim", ogrenci_not.ders_id);
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler.Where(o => o.silindiMi == false && o.devamDurumu == true), "id", "adSoyad", ogrenci_not.ogrenci_id);
                return View(ogrenci_not);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: ogrenci_notlar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,not,ders_id,donem_id,ogrenci_id,silindiMi")] ogrenci_not ogrenci_not)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && Session["kulId"] != null || Convert.ToInt32(Session["kulTip"]) == 2 && Session["kulId"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(ogrenci_not).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("index","ogrenci_notlar");
                }
                ViewBag.ders_id = new SelectList(db.dersler, "id", "isim", ogrenci_not.ders_id);
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad", ogrenci_not.ogrenci_id);
                return View(ogrenci_not);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogrenci_notlar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci_not ogrenci_not = db.ogrenciNotlar.Find(id);
                if (ogrenci_not == null)
                {
                    return HttpNotFound();
                }
                ogrenci_not.silindiMi = true;
                db.Entry(ogrenci_not).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "ogrenci_notlar");
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
