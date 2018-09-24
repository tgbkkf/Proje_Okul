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
    public class duyurularController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: duyurular
        public ActionResult Index()
        {

            if (Convert.ToInt32(Session["kulTip"]) == 1 && Session["kulId"] != null || Convert.ToInt32(Session["kulTip"]) == 2 && Session["kulId"] != null)
            {
                return View(db.duyurular.Where(x => x.silindiMi == false).ToList());
            }

            else
            {
                return RedirectToAction("index", "giris");
            }

        }



        // GET: duyurular/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            duyuru duyuru = db.duyurular.Find(id);
            if (duyuru == null)
            {
                return HttpNotFound();
            }
            return View(duyuru);
        }

        // GET: duyurular/Create
        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && Session["kulId"] != null || Convert.ToInt32(Session["kulTip"]) == 2 && Session["kulId"] != null)
            {
                return View();
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: duyurular/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,mesaj,tarihi,aktifMi,silindiMi")] duyuru duyuru)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && Session["kulId"] != null || Convert.ToInt32(Session["kulTip"]) == 2 && Session["kulId"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.duyurular.Add(duyuru);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(duyuru);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

     
        // GET: duyurular/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && Session["kulId"] != null || Convert.ToInt32(Session["kulTip"]) == 2 && Session["kulId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                duyuru duyuru = db.duyurular.Find(id);
                if (duyuru == null)
                {
                    return HttpNotFound();
                }
                return View(duyuru);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: duyurular/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,mesaj,tarihi,aktifMi,silindiMi")] duyuru duyuru)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && Session["kulId"] != null || Convert.ToInt32(Session["kulTip"]) == 2 && Session["kulId"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(duyuru).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(duyuru);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: duyurular/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && Session["kulId"] != null || Convert.ToInt32(Session["kulTip"]) == 2 && Session["kulId"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                duyuru duyuru = db.duyurular.Find(id);
                if (duyuru == null)
                {
                    return HttpNotFound();
                }
                duyuru.silindiMi = true;
                db.Entry(duyuru).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "duyurular");
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
