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
using System.IO;

namespace BilgeKoleji.Controllers
{
    public class ogrenci_odevlerController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: ogrenci_odevler
        public ActionResult Index()
        {
            if (Session["kulId"] != null)
            {
                if (Convert.ToInt32(Session["kulTip"]) == 3)
                {
                    int kullanici = Convert.ToInt32(db.kullanicilar.Find(Convert.ToInt32(Session["kulId"])).ogrenci_id);
                    var ogrenci_odevler = db.ogrenciOdevler.Where(x => x.ogrenci_id == kullanici && x.silindiMi==false).ToList();
                    return View(ogrenci_odevler);
                }
                else if (Convert.ToInt32(Session["kulTip"]) == 1)
                {
                    int kullanici = Convert.ToInt32(db.kullanicilar.Find(Convert.ToInt32(Session["kulId"])).ogrenci_id);
                    var ogrenci_odevler = db.ogrenciOdevler.Where(x =>x.silindiMi==false && x.ogrenci.silindiMi == false && x.ogrenci.devamDurumu==true).ToList();
                    return View(ogrenci_odevler);
                }
                else
                {
                    return RedirectToAction("index", "giris");
                }
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

                int ogretmen_id = Convert.ToInt32(db.kullanicilar.Find(Convert.ToInt32(Session["kulId"])).ogretmen_id);
                List<ogrenci_odev> ooList = new List<ogrenci_odev>();
                List<sube> sList = new List<sube>();
                sList = db.ogretmen.Find(db.kullanicilar.Find(Convert.ToInt32(Session["kulId"])).ogretmen_id).ogretmenSubeler.Where(os => os.ogretmen.silindiMi == false && os.sube.silindiMi == false && os.silindiMi == false).Select(os => os.sube).ToList();


                foreach (var item in sList)
                {
                    foreach (var itemOgrenci in item.ogrenciSubeler.Where(os => os.silindiMi == false && os.sube.silindiMi == false && os.ogrenci.silindiMi == false && os.ogrenci.devamDurumu == true).Select(os => os.ogrenci))
                    {
                        foreach (var itemOdev in itemOgrenci.ogrenciOdevler.Where(oo => oo.silindiMi == false && oo.ogrenci.silindiMi == false && oo.ogrenci.devamDurumu == true))
                        {
                            ogrenci_odev odev = new ogrenci_odev();
                            odev = itemOdev;
                            ooList.Add(odev);
                        }
                    }

                }

                return View(ooList);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

        }


        // GET: ogrenci_odevler/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 3 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci_odev ogrenci_odev = db.ogrenciOdevler.Find(id);
                if (ogrenci_odev == null)
                {
                    return HttpNotFound();
                }
                return View(ogrenci_odev);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogrenci_odevler/Create
        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 3 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                ViewBag.ders_id = new SelectList(db.dersler, "id", "isim");
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad");
                return View();
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: ogrenci_odevler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ders_id,silindiMi")] ogrenci_odev ogrenci_odev, HttpPostedFileBase file)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 3 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    string path = "";
                    string filePath = "";

                    if (file != null && file.ContentLength > 0)
                        try
                        {
                            path = Path.Combine(Server.MapPath("~/Odevler/"),
                                                         Path.GetFileName(file.FileName));
                            filePath = "~/Odevler/" + file.FileName;
                            file.SaveAs(path);
                            //ViewBag.Message = "Dosya yükleme başarılı";
                        }
                        catch (Exception ex)
                        {
                            //ViewBag.Message = "Hata:" + ex.Message.ToString();
                        }
                    else
                    {
                        //ViewBag.Message = "Lütfen dosya seçiniz.";
                    }
                    ogrenci_odev.donem = db.donemler.FirstOrDefault(d => d.baslangicTarihi <= DateTime.Now && d.bitisTarihi > DateTime.Now);
                    ogrenci_odev.donem_id = ogrenci_odev.donem.id;
                    ogrenci_odev.dosyaYolu = filePath;
                    ogrenci_odev.ogrenci_id = Convert.ToInt32(db.kullanicilar.Find(Convert.ToInt32(Session["kulId"])).ogrenci_id);
                    db.ogrenciOdevler.Add(ogrenci_odev);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ders_id = new SelectList(db.dersler, "id", "isim", ogrenci_odev.ders_id);
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad", ogrenci_odev.ogrenci_id);
                return View(ogrenci_odev);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }




        // GET: ogrenci_odevler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 3 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci_odev ogrenci_odev = db.ogrenciOdevler.Find(id);
                if (ogrenci_odev == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ders_id = new SelectList(db.dersler, "id", "isim", ogrenci_odev.ders_id);
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad", ogrenci_odev.ogrenci_id);
                return View(ogrenci_odev);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: ogrenci_odevler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,dosyaYolu,ogrenci_id,ders_id,donem_id,silindiMi")] ogrenci_odev ogrenci_odev, HttpPostedFileBase file)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 3 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    string path = "";
                    string filePath = "";

                    if (file != null && file.ContentLength > 0)
                        try
                        {
                            path = Path.Combine(Server.MapPath("~/Odevler/"),
                                                         Path.GetFileName(file.FileName));
                            filePath = "~/Odevler/" + file.FileName;
                            file.SaveAs(path);
                            //ViewBag.Message = "Dosya yükleme başarılı";
                        }
                        catch (Exception ex)
                        {
                            //ViewBag.Message = "Hata:" + ex.Message.ToString();
                        }
                    else
                    {
                        //ViewBag.Message = "Lütfen dosya seçiniz.";
                    }
                    ogrenci_odev.ogrenci_id = Convert.ToInt32(db.kullanicilar.Find(Convert.ToInt32(Session["kulId"])).ogrenci_id);
                    ogrenci_odev.dosyaYolu = filePath;

                    db.Entry(ogrenci_odev).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ders_id = new SelectList(db.dersler, "id", "isim", ogrenci_odev.ders_id);
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler, "id", "adSoyad", ogrenci_odev.ogrenci_id);
                return View(ogrenci_odev);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogrenci_odevler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 3 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci_odev ogrenci_odev = db.ogrenciOdevler.Find(id);
                if (ogrenci_odev == null)
                {
                    return HttpNotFound();
                }
                ogrenci_odev.silindiMi = true;
                db.Entry(ogrenci_odev).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "ogrenci_odevler");
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
