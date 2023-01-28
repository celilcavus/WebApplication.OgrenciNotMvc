using OgrenciNotMvc.Models.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace OgrenciNotMvc.Controllers
{
    public class DefaultController : Controller
    {
        DbMvcOkulEntities1 db = new DbMvcOkulEntities1();
        public ActionResult Index()
        {
            var dersler = db.TBLDERSLER.ToList();
            return View(dersler);
        }

        [HttpGet]
        public ActionResult DersAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DersAdd(TBLDERSLER ders)
        {
            TBLDERSLER d = new TBLDERSLER();
            d.DERSAD = ders.DERSAD.ToUpper();
            db.TBLDERSLER.Add(d);
            int addcontrol = db.SaveChanges();
            if (addcontrol >= 1)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }

        public ActionResult Sil(int id)
        {
            var dersler = db.TBLDERSLER.Where(x => x.DERSID == id).FirstOrDefault();
            db.TBLDERSLER.Remove(dersler);
            db.SaveChanges();
            return RedirectToAction("Index", "Default");
        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var value = db.TBLDERSLER.Find(id);
            return View("Guncelle", value);
        }
        [HttpPost]
        public ActionResult Guncelle(TBLDERSLER ders)
        {
            var id = db.TBLDERSLER.Find(ders.DERSID);
            id.DERSAD = ders.DERSAD.ToUpper();
            int addcontrol = db.SaveChanges();
            if (addcontrol >= 1)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }
    }
}