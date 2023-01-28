using OgrenciNotMvc.Models.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace OgrenciNotMvc.Controllers
{
    public class OgrenciController : Controller
    {
        DbMvcOkulEntities1 db = new DbMvcOkulEntities1();

        public ActionResult Index()
        {
            var ogrenci = db.TBLOGRENCILER.ToList();
            return View(ogrenci);
        }
        [HttpGet]
        public void ogrencigetir()
        {
            List<SelectListItem> ogrenciler = (from i in db.TBLKULUPLER.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.KULUPAD,
                                                   Value = i.KULUPID.ToString()
                                               }).ToList();
            ViewBag.dgr = ogrenciler;
        }
        public ActionResult OgrenciAdd()
        {
            ogrencigetir();
            return View();
        }

        [HttpPost]
        public ActionResult OgrenciAdd(TBLOGRENCILER ogr)
        {
            var klp = db.TBLKULUPLER.Where(x => x.KULUPID == ogr.TBLKULUPLER.KULUPID).FirstOrDefault();
            TBLOGRENCILER o = new TBLOGRENCILER();

            o.OGRAD = ogr.OGRAD.ToUpper();
            o.OGRSOYAD = ogr.OGRSOYAD.ToUpper();
            o.OGRFOTOGRAF = ogr.OGRFOTOGRAF.ToUpper();
            o.OGRCINSIYET = ogr.OGRCINSIYET.ToUpper();
            o.TBLKULUPLER = klp;
            db.TBLOGRENCILER.Add(o);
            int control = db.SaveChanges();
            if (control >= 1)
            {
                return RedirectToAction("Index", "Ogrenci");
            }
            return View();
        }

        public ActionResult Sil(int id)
        {
            var ogrenci = db.TBLOGRENCILER.Where(x => x.OGRENCIID == id).FirstOrDefault();
            db.TBLOGRENCILER.Remove(ogrenci);
            db.SaveChanges();
            return RedirectToAction("Index", "Ogrenci");
        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            ogrencigetir();
            var value = db.TBLOGRENCILER.Where(x => x.OGRENCIID == id).FirstOrDefault();
            return View(value);
        }
        [HttpPost]
        public ActionResult Guncelle(TBLOGRENCILER ogr)
        {
            var k = db.TBLKULUPLER.Where(x => x.KULUPID == ogr.TBLKULUPLER.KULUPID).FirstOrDefault();

            var klp = db.TBLOGRENCILER.Find(ogr.OGRENCIID);
            klp.OGRAD = ogr.OGRAD.ToUpper();
            klp.OGRSOYAD = ogr.OGRSOYAD.ToUpper();
            klp.OGRFOTOGRAF = ogr.OGRFOTOGRAF.ToUpper();
            klp.OGRCINSIYET = ogr.OGRCINSIYET.ToUpper();
            klp.TBLKULUPLER = k;
            int control = db.SaveChanges();
            if (control >= 1)
            {
                return RedirectToAction("Index", "Ogrenci");
            }
            return View();
        }
    }
}