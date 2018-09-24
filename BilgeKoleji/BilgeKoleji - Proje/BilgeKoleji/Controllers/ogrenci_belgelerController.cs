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
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace BilgeKoleji.Controllers
{
    public class ogrenci_belgelerController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: ogrenci_belgeler
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                var ogrenciBelgeler = db.ogrenciBelgeler.Include(o => o.ogrenci);
                return View(ogrenciBelgeler.Where(x => x.silindiMi == false).ToList());
            }
            else
            {
                return RedirectToAction("index", "giris");
            }


        }

        // GET: ogrenci_belgeler/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci_belge ogrenci_belge = db.ogrenciBelgeler.Find(id);
                if (ogrenci_belge == null)
                {
                    return HttpNotFound();
                }
                return View(ogrenci_belge);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogrenci_belgeler/Create
        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                ViewBag.ogrenci_id = new SelectList(db.ogrenciler.Where(o => o.silindiMi == false && o.devamDurumu == true), "id", "adSoyad");
                ViewBag.belge_id = new SelectList(db.belgeler.Where(b => b.silindiMi == false), "id", "isim");
                return View();
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: ogrenci_belgeler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,belge_id,ogrenci_id,silindiMi")] ogrenci_belge ogrenci_belge)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (ModelState.IsValid)
                {

                    ogrenci_belge.ogrenci = db.ogrenciler.Find(ogrenci_belge.ogrenci_id);
                    ogrenci_belge.belge = db.belgeler.Find(ogrenci_belge.belge_id);
                    ogrenci_belge.donem = db.donemler.FirstOrDefault(d => d.baslangicTarihi <= DateTime.Now && d.bitisTarihi > DateTime.Now);
                    ogrenci_belge.donem_id = ogrenci_belge.donem.id;
                    //http://stackoverflow.com/questions/6826921/write-text-on-an-image-in-c-sharp
                    if (ogrenci_belge.belge.isim == "Takdir Belgesi")
                    {
                        string firstText = "Okulumuzun öğrencilerinden" + ogrenci_belge.ogrenci.okulNo + "numaralı" + ogrenci_belge.ogrenci.adSoyad;
                        string secondText = ogrenci_belge.donem.baslangicTarihi.ToShortDateString() + " - " + ogrenci_belge.donem.bitisTarihi.ToShortDateString() + "döneminde okul içinde ve dışında";
                        string thirdText = "göstermiş olduğu erdemli davranışlarından bu takdir belgesini almaya hak kazanmıştır.";
                        PointF firstLocation = new PointF(10f, 10f);
                        PointF secondLocation = new PointF(10f, 50f);
                        PointF thirdLocation = new PointF(10f, 90f);
                        string imageFilePath = ogrenci_belge.belge.belgeYolu;
                        Bitmap bitmap = (Bitmap)Image.FromFile(Path.Combine(Server.MapPath(imageFilePath)));//load the image file

                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            using (Font arialFont = new Font("Arial", 10))
                            {
                                graphics.DrawString(firstText, arialFont, Brushes.Black, firstLocation);
                                graphics.DrawString(secondText, arialFont, Brushes.Black, secondLocation);
                                graphics.DrawString(thirdText, arialFont, Brushes.Black, thirdLocation);
                            }
                        }

                        string ogrenciBelge = "~/OgrenciBelgeler/";

                        //bitmap.Save(Path.Combine(Server.MapPath(ogrenciBelge)));//save the image file
                        //bitmap.Save(@"C:\");
                    }




                    ogrenci_belge.donem_id = db.donemler.FirstOrDefault(d => d.baslangicTarihi <= DateTime.Now && d.bitisTarihi > DateTime.Now).id;

                    db.ogrenciBelgeler.Add(ogrenci_belge);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ogrenci_id = new SelectList(db.ogrenciler.Where(o => o.silindiMi == false), "id", "adSoyad", ogrenci_belge.ogrenci_id);
                ViewBag.belge_id = new SelectList(db.belgeler.Where(b => b.silindiMi == false), "id", "isim", ogrenci_belge.belge_id);
                return View(ogrenci_belge);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogrenci_belgeler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci_belge ogrenci_belge = db.ogrenciBelgeler.Find(id);
                if (ogrenci_belge == null)
                {
                    return HttpNotFound();
                }

                ViewBag.ogrenci_id = new SelectList(db.ogrenciler.Where(o => o.silindiMi == false && o.devamDurumu == true), "id", "adSoyad", ogrenci_belge.ogrenci_id);
                ViewBag.belge_id = new SelectList(db.belgeler.Where(b => b.silindiMi == false), "id", "isim", ogrenci_belge.belge_id);
                return View(ogrenci_belge);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: ogrenci_belgeler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,belgeYolu,belge_id,ogrenci_id,silindiMi")] ogrenci_belge ogrenci_belge)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (ModelState.IsValid)
                {
                    db.Entry(ogrenci_belge).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ogrenci_id = new SelectList(db.ogrenciler.Where(o => o.silindiMi == false), "id", "adSoyad", ogrenci_belge.ogrenci_id);
                ViewBag.belge_id = new SelectList(db.belgeler.Where(b => b.silindiMi == false), "id", "isim", ogrenci_belge.belge_id);
                return View(ogrenci_belge);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: ogrenci_belgeler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ogrenci_belge ogrenci_belge = db.ogrenciBelgeler.Find(id);
                if (ogrenci_belge == null)
                {
                    return HttpNotFound();
                }
                ogrenci_belge.silindiMi = true;
                db.Entry(ogrenci_belge).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "ogrenci_belgeler");
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


