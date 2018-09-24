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
    public class derslerController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: dersler
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                return View(db.dersler.Where(d => d.silindiMi == false).ToList());
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

           
        }
           
      

        // GET: dersler/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ders ders = db.dersler.Find(id);
                if (ders == null)
                {
                    return HttpNotFound();
                }
                return View(ders);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // GET: dersler/Create
        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                ViewBag.ders_id = new SelectList(db.dersler, "id", "isim");
                return View();
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // POST: dersler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,isim,ders_id,haftalikSaat,silindiMi")] ders ders)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (ModelState.IsValid)
            {
                db.dersler.Add(ders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ders_id = new SelectList(db.dersler, "id", "isim", ders.ders_id);
            return View(ders);
            }

         else
            {
                return RedirectToAction("index", "giris");
             }

        }

            // GET: dersler/Edit/5
            public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ders ders = db.dersler.Find(id);
                if (ders == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ders_id = new SelectList(db.dersler, "id", "isim", ders.ders_id);
                return View(ders);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: dersler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,isim,ders_id,haftalikSaat,silindiMi")] ders ders)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (ModelState.IsValid)
            {
                db.Entry(ders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ders_id = new SelectList(db.dersler, "id", "isim", ders.ders_id);
            return View(ders);
        }
         else
            {
                return RedirectToAction("index", "giris");
    }
}

        // GET: dersler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ders ders = db.dersler.Find(id);
                if (ders == null)
                {
                    return HttpNotFound();
                }
                ders.silindiMi = true;
                db.Entry(ders).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "dersler");
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
