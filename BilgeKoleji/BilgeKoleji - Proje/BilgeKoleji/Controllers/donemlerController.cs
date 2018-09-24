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
    public class donemlerController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: donemler
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                return View(db.donemler.Where(x => x.silindiMi == false).ToList());
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // GET: donemler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            donem donem = db.donemler.Find(id);
            if (donem == null)
            {
                return HttpNotFound();
            }
            return View(donem);
        }

        // GET: donemler/Create
        public ActionResult Create()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                return View();
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // POST: donemler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,isim,baslangicTarihi,bitisTarihi,silindiMi")] donem donem)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            { 
                if (ModelState.IsValid)
            {
                db.donemler.Add(donem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

                return View(donem);
             }
                 else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: donemler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                donem donem = db.donemler.Find(id);
                if (donem == null)
                {
                    return HttpNotFound();
                }
                return View(donem);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // POST: donemler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,isim,baslangicTarihi,bitisTarihi,silindiMi")] donem donem)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(donem).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(donem);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: donemler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                donem donem = db.donemler.Find(id);
                if (donem == null)
                {
                    return HttpNotFound();
                }
                donem.silindiMi = true;
                db.Entry(donem).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "donemler");
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
