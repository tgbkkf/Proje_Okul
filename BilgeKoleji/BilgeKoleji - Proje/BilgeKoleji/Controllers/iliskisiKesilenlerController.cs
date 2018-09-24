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
    public class iliskisiKesilenlerController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: iliskisiKesilenler
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                var iliskisiKesilenler = db.iliskisiKesilenler.Include(i => i.veli);
                return View(iliskisiKesilenler.Where(x => x.silindiMi == false).ToList());
               
            }

            else
            {
                return RedirectToAction("index", "giris");
            }

          
        }

        // GET: iliskisiKesilenler/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iliskisiKesilen iliskisiKesilen = db.iliskisiKesilenler.Find(id);
            if (iliskisiKesilen == null)
            {
                return HttpNotFound();
            }
            return View(iliskisiKesilen);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: iliskisiKesilenler/Create
        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                ViewBag.veli_id = new SelectList(db.veliler, "id", "adSoyad");

                return View();
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: iliskisiKesilenler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,adSoyad,okulNo,cinsiyet,bitirdigiOkul,notOrtalamsi,veli_id,silindiMi")] iliskisiKesilen iliskisiKesilen)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (ModelState.IsValid)
            {
                db.iliskisiKesilenler.Add(iliskisiKesilen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.veli_id = new SelectList(db.veliler, "id", "adSoyad", iliskisiKesilen.veli_id);
            return View(iliskisiKesilen);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: iliskisiKesilenler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iliskisiKesilen iliskisiKesilen = db.iliskisiKesilenler.Find(id);
            if (iliskisiKesilen == null)
            {
                return HttpNotFound();
            }
            ViewBag.veli_id = new SelectList(db.veliler, "id", "adSoyad", iliskisiKesilen.veli_id);
                return View(iliskisiKesilen);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }
}

        // POST: iliskisiKesilenler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,adSoyad,okulNo,cinsiyet,bitirdigiOkul,notOrtalamsi,veli_id,silindiMi")] iliskisiKesilen iliskisiKesilen)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (ModelState.IsValid)
            {
                db.Entry(iliskisiKesilen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.veli_id = new SelectList(db.veliler, "id", "adSoyad", iliskisiKesilen.veli_id);
            return View(iliskisiKesilen);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }
}

        // GET: iliskisiKesilenler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iliskisiKesilen iliskisiKesilen = db.iliskisiKesilenler.Find(id);
            if (iliskisiKesilen == null)
            {
                return HttpNotFound();
            }
                iliskisiKesilen.silindiMi = true;
                db.Entry(iliskisiKesilen).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "iliskisiKesilenler");
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
