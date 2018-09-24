using BilgeKoleji.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BilgeKoleji.Controllers
{
    public class girisController : Controller
    {

        BilgeDb db = new BilgeDb();
        public ActionResult index()
        {
            return View();
        }
        // POST: giris/Create
        [HttpPost]
        public ActionResult index([Bind(Include = "kulAdi,sifre")]giris giris)
        {

            try
            {
                kullanici kul = new kullanici();
                if (db.kullanicilar.Where(k => k.kulAdi == giris.kulAdi && k.password == giris.sifre).Count() != 0)
                {
                    // 
                    int kulTipi;
                    kul = db.kullanicilar.Where(k => k.kulAdi == giris.kulAdi && k.password == giris.sifre).First();

                    Session["kulId"] = kul.id;
                    if (kul.ogrenci_id != null)
                    {
                        kulTipi = 3;
                        Session["kulAdi"] = kul.ogrenci.adSoyad;
                    }
                    else if (kul.ogretmen_id != null)
                    {
                        kulTipi = 2;
                        Session["kulAdi"] = kul.ogretmen.adiSoyadi;
                    }
                    else if (kul.veli_id != null)
                    {

                        Session["kulAdi"] = kul.veli.adSoyad;
                        kulTipi = 4;
                    }
                    else
                    {
                        kulTipi = 1;
                        Session["kulAdi"] = kul.kulAdi;
                    }

                    Session["kulTip"] = kulTipi;

                    switch (kulTipi)
                    {
                        case 1: return RedirectToAction("index", "anasayfa");
                        case 2: return RedirectToAction("IndexOgretmen", "ogretmen_subeler");
                        case 3: return RedirectToAction("index", "anasayfa");
                        case 4:return RedirectToAction("OgrenciDetay", "veli_ogrenci");
                        default:
                            return RedirectToAction("Index");
                            break;
                    }

                }
                else
                {
                    return RedirectToAction("Index");
                }


            }
            catch
            {
                return RedirectToAction("Index");
            }
        }




        public ActionResult Logout()
        {
            kullanici kul = new kullanici();

            Session.RemoveAll();

            return RedirectToAction("Index", "home");
        }
        

       

    }
}
