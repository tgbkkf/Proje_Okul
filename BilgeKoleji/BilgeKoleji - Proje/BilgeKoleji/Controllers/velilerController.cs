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
    public class velilerController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: veliler
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                return View(db.veliler.Where(x => x.silindiMi == false).ToList());
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

            
        }

        // GET: veliler/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                veli veli = db.veliler.Find(id);
                if (veli == null)
                {
                    return HttpNotFound();
                }
                return View(veli);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: veliler/Create
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

        // POST: veliler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,adSoyad,tcKimlik,evTelefonu,isTelefonu,adres,ilIlce,silindiMi")] veli veli)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {

                if (ModelState.IsValid)
                {
                    db.veliler.Add(veli);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(veli);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: veliler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                veli veli = db.veliler.Find(id);
                if (veli == null)
                {
                    return HttpNotFound();
                }
                return View(veli);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // POST: veliler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,adSoyad,tcKimlik,evTelefonu,isTelefonu,adres,ilIlce,silindiMi")] veli veli)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(veli).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(veli);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: veliler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                veli veli = db.veliler.Find(id);
                if (veli == null)
                {
                    return HttpNotFound();
                }
                veli.silindiMi = true;
                db.Entry(veli).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("index", "veliler");
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
