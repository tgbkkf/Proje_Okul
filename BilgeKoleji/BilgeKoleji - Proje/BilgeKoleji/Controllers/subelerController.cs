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
    public class subelerController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: subeler
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                return View(db.subeler.Where(x => x.silindiMi == false).ToList());
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

          
        }

        // GET: subeler/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                sube sube = db.subeler.Find(id);
                if (sube == null)
                {
                    return HttpNotFound();
                }
                return View(sube);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: subeler/Create
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

        // POST: subeler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,isim,sinif,silindiMi")] sube sube)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    db.subeler.Add(sube);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(sube);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: subeler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                sube sube = db.subeler.Find(id);
                if (sube == null)
                {
                    return HttpNotFound();
                }
                return View(sube);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: subeler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,isim,sinif,silindiMi")] sube sube)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(sube).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sube);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: subeler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                sube sube = db.subeler.Find(id);
                if (sube == null)
                {
                    return HttpNotFound();
                }
                sube.silindiMi = true;
                db.Entry(sube).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "subeler");
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
