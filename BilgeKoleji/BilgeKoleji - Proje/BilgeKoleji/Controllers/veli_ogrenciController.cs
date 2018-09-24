using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BilgeKoleji.Models;
using System.Web.Mvc;

namespace BilgeKoleji.Controllers
{
    public class veli_ogrenciController : Controller
    {
        BilgeDb db = new BilgeDb();
        // GET: veli_ogrenci_notlar
        public ActionResult Index()
        {

            if (Convert.ToInt32(Session["kulTip"]) == 4 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {

                int kullanici = Convert.ToInt32(db.kullanicilar.Find(Convert.ToInt32(Session["kulId"])).veli_id);
                var ogrenciler = db.ogrenciler.Where(o => o.veli_id == kullanici && o.silindiMi == false && o.devamDurumu==true && o.onKayitMi==false);
                return View(ogrenciler.ToList());
            }
            else
            {
                return RedirectToAction("index", "giris");
            }
        }



        public ActionResult OgrenciDetay()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 4 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {

                var ogrenciList = new List<ogrenci>();
                var notlarList = new List<ogrenci_not>();
                int veliId = Convert.ToInt32(db.kullanicilar.Find(Convert.ToInt32(Session["kulId"])).veli_id);
                ogrenciList = db.ogrenciler.Where(o => o.veli_id == veliId && o.silindiMi == false).ToList();
                foreach (var item in ogrenciList)
                {
                    foreach (var item2 in item.ogrenciNotlar)
                    {
                        ogrenci_not on = new ogrenci_not();
                        on = item2;
                        notlarList.Add(on);
                    }


                }
                return View(notlarList.Where(on=> on.silindiMi == false));
            }
            else
            {
                return RedirectToAction("index", "giris");
            }
        }

        public ActionResult OgrenciDevamsizlik()
        {
            if (Convert.ToInt32(Session["kulTip"]) == 4 && !String.IsNullOrEmpty(Session["kulId"].ToString()))
            {
                var ogrenciList = new List<ogrenci>();
                var devamsizlikList = new List<ogrenci_devamsizlik>();
                int veliId = Convert.ToInt32(db.kullanicilar.Find(Convert.ToInt32(Session["kulId"])).veli_id);
                ogrenciList = db.ogrenciler.Where(o => o.veli_id == veliId && o.silindiMi==false).ToList();
                foreach (var item in ogrenciList)
                {
                    foreach (var item2 in item.ogrenciDevamsizliklar)
                    {
                        ogrenci_devamsizlik od = new ogrenci_devamsizlik();
                        od = item2;
                        devamsizlikList.Add(od);
                    }

                }
                return View(devamsizlikList.Where(od => od.silindiMi == false));
            }

            else
            {
                return RedirectToAction("index", "giris");
            }

        }
        

        public ActionResult Duyurular()
        {
           
                var duyurular = db.duyurular.Where(d => d.silindiMi == false && d.aktifMi == true);
                return View(duyurular.ToList());
            }

           
        }
    }

