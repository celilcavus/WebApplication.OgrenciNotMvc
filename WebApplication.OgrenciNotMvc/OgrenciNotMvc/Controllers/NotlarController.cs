using OgrenciNotMvc.Models.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OgrenciNotMvc.Controllers
{
    public class NotlarController : Controller
    {
        DbMvcOkulEntities1 db = new DbMvcOkulEntities1();
        public ActionResult Index()
        {
            var notlar = db.TBLNOTLAR.ToList();
            return View(notlar);
        }
        [HttpGet]
        public ActionResult notAdd()
        {
            List<SelectListItem> ogrenciler = (from i in db.TBLOGRENCILER.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.OGRAD,
                                                   Value = i.OGRENCIID.ToString()
                                               }).ToList();
            ViewBag.ogr = ogrenciler;

            List<SelectListItem> dersler = (from i in db.TBLDERSLER.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.DERSAD,
                                                Value = i.DERSID.ToString()
                                            }).ToList();
            ViewBag.ders = dersler;
            return View();
        }

        [HttpPost]
        public ActionResult notAdd(TBLNOTLAR nt)
        {
            var ogr = db.TBLOGRENCILER.Where(x => x.OGRENCIID == nt.OGRID).FirstOrDefault();
            var ders = db.TBLDERSLER.Where(x => x.DERSID == nt.DERSID).FirstOrDefault();
            TBLNOTLAR not = new TBLNOTLAR();
            not.TBLOGRENCILER = ogr;
            not.TBLDERSLER = ders;
            not.SINAV1 = nt.SINAV1;
            not.SINAV2 = nt.SINAV2;
            not.SINAV3 = nt.SINAV3;
            not.ORTALAMA = nt.ORTALAMA;
            not.DURUM = nt.DURUM;
            db.TBLNOTLAR.Add(not);
            int i = db.SaveChanges();
            if (i >= 1)
            {
                return RedirectToAction("Index", "Notlar");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {

            var value = db.TBLNOTLAR.Find(id);

            List<SelectListItem> ogrenciler = (from i in db.TBLOGRENCILER.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.OGRAD,
                                                   Value = i.OGRENCIID.ToString()
                                               }).ToList();
            ViewBag.ogr = ogrenciler;

            List<SelectListItem> dersler = (from i in db.TBLDERSLER.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.DERSAD,
                                                Value = i.DERSID.ToString()
                                            }).ToList();
            ViewBag.ders = dersler;
            return View(value);
        }

        [HttpPost]
        public ActionResult Guncelle(TBLNOTLAR nt)
        {
            var not = db.TBLNOTLAR.Find(nt.NOTID);
            var ogr = db.TBLOGRENCILER.Where(x => x.OGRENCIID == nt.OGRID).FirstOrDefault();
            var ders = db.TBLDERSLER.Where(x => x.DERSID == nt.DERSID).FirstOrDefault();
            
            not.TBLOGRENCILER = ogr;
            not.TBLDERSLER = ders;
            not.SINAV1 = nt.SINAV1;
            not.SINAV2 = nt.SINAV2;
            not.SINAV3 = nt.SINAV3;
            not.ORTALAMA = nt.ORTALAMA;
            not.DURUM = nt.DURUM;
            int i = db.SaveChanges();
            if (i >= 1)
            {
                return RedirectToAction("Index", "Notlar");
            }
            return View();
        }
    }
}