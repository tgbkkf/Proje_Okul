using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BilgeKoleji.Controllers
{
    public class homeController : Controller
    {
        BilgeDb db = new BilgeDb();
        // GET: home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult Duyurular()
        {

            return View(db.duyurular.Where(d => d.silindiMi == false && d.aktifMi == true));
        }
        public ActionResult Iletisim()
        {
            return View();
        }
        public ActionResult Giris()
        {
            return View("index", "giris");
        }
    }
}