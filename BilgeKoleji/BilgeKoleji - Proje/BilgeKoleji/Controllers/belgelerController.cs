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
    public class belgelerController : Controller
    {
        private BilgeDb db = new BilgeDb();

        // GET: belgeler
        public ActionResult Index()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))

            {
                return View(db.belgeler.Where(x => x.silindiMi == false).ToList());
            }
            else
            {
                return RedirectToAction("index", "giris");
            }


        }

        // GET: belgeler/Details/5
        public ActionResult Details(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                belge belge = db.belgeler.Find(id);
                if (belge == null)
                {
                    return HttpNotFound();
                }
                return View(belge);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }


        }

        // GET: belgeler/Create
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

        // POST: belgeler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,isim,belgeYolu,silindiMi")] belge belge,HttpPostedFileBase file)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {
                    string path = "";
                    string filePath = "";
                    if (file != null && file.ContentLength > 0)
                        try
                        {
                            path = Path.Combine(Server.MapPath("~/Belgeler/"),
                                                         Path.GetFileName(file.FileName));
                            filePath = "~/Belgeler/" + file.FileName;
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
                    belge.belgeYolu = filePath;
                    db.belgeler.Add(belge);
                    db.SaveChanges();
                    return Redirect("~/belgeler/index");
                }

                return View(belge);
            }

            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        // GET: belgeler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                belge belge = db.belgeler.Find(id);
                if (belge == null)
                {
                    return HttpNotFound();
                }
                return View(belge);
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // POST: belgeler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,belgeYolu,isim,silindiMi")] belge belge, HttpPostedFileBase file)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (ModelState.IsValid)
                {

                    
                    if (file != null && file.ContentLength > 0)
                        try
                        {
                            string path = "";
                            string filePath = "";
                            path = Path.Combine(Server.MapPath("~/Belgeler/"),
                                                         Path.GetFileName(file.FileName));
                            filePath = "~/Belgeler/" + file.FileName;
                            file.SaveAs(path);
                            //ViewBag.Message = "Dosya yükleme başarılı";
                            belge.belgeYolu = filePath;
                        }
                        catch (Exception ex)
                        {
                            //ViewBag.Message = "Hata:" + ex.Message.ToString();
                        }
                    else
                    {
                        //ViewBag.Message = "Lütfen dosya seçiniz.";
                    }

                   
                    db.Entry(belge).State = EntityState.Modified;
                    db.SaveChanges();
                   
                    return Redirect("~/belgeler/index");
                }
                return RedirectToAction("index", "belgeler");
            }
            else
            {
                return RedirectToAction("index", "giris");
            }

        }

        // GET: belgeler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Convert.ToInt32(Session["kulTip"]) == 1 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                belge belge = db.belgeler.Find(id);
                if (belge == null)
                {
                    return HttpNotFound();
                }
                belge.silindiMi = true;
                db.Entry(belge).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("~/belgeler/index");
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
