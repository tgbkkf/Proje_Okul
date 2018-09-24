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
    public class devamsizliklarController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: devamsizliklar
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                return View(db.devamsizliklar.Where(d => d.silindiMi == false).ToList());
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // GET: devamsizliklar/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                devamsizlik devamsizlik = db.devamsizliklar.Find(id);
                if (devamsizlik == null)
                {
                    return HttpNotFound();
                }
                return View(devamsizlik);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }

        }




        // GET: devamsizliklar/Create
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

// POST: devamsizliklar/Create
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,silindiMi")] devamsizlik devamsizlik)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    db.devamsizliklar.Add(devamsizlik);
                    db.SaveChanges();
                    return RedirectToAction("index");
                }

                return View(devamsizlik);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: devamsizliklar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            devamsizlik devamsizlik = db.devamsizliklar.Find(id);
            if (devamsizlik == null)
            {
                return HttpNotFound();
            }
            return View(devamsizlik);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }


      }

// POST: devamsizliklar/Edit/5
// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,silindiMi")] devamsizlik devamsizlik)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (ModelState.IsValid)
                {
                    db.Entry(devamsizlik).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(devamsizlik);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // GET: devamsizliklar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                devamsizlik devamsizlik = db.devamsizliklar.Find(id);
                if (devamsizlik == null)
                {
                    return HttpNotFound();
                }
                devamsizlik.silindiMi = true;
                db.Entry(devamsizlik).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index","devamsizliklar");
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
