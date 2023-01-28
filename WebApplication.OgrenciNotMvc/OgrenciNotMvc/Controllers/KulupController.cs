using OgrenciNotMvc.Models.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace OgrenciNotMvc.Controllers
{
    public class KulupController : Controller
    {

        DbMvcOkulEntities1 db = new DbMvcOkulEntities1();
        public ActionResult Index()
        {
            var kulup = db.TBLKULUPLER.ToList();
            return View(kulup);
        }
        [HttpGet]
        public ActionResult kulupAdd()
        {

            return View();
        }
        [HttpPost]
        public ActionResult kulupAdd(TBLKULUPLER kulup)
        {
            TBLKULUPLER k = new TBLKULUPLER();
            k.KULUPAD = kulup.KULUPAD.ToUpper();
            k.KULUPKONTEJAN = kulup.KULUPKONTEJAN;
            db.TBLKULUPLER.Add(k);
            int kcontrol = db.SaveChanges();
            if (kcontrol >= 1)
            {
                return RedirectToAction("Index", "Kulup");
            }
            return View();
        }

        public ActionResult Sil(TBLKULUPLER k)
        {
            var KulupSil = db.TBLKULUPLER.Where(x => x.KULUPID == k.KULUPID).FirstOrDefault();
            db.TBLKULUPLER.Remove(KulupSil);
            db.SaveChanges();
            return RedirectToAction("Index", "kulup");

        }

        [HttpGet]
        public ActionResult Guncelle(int id)
        {
            var value = db.TBLKULUPLER.Where(x => x.KULUPID == id).FirstOrDefault();
            return View(value);
        }
        [HttpPost]
        public ActionResult Guncelle(TBLKULUPLER p)
        {
            var id = db.TBLKULUPLER.Find(p.KULUPID);
            id.KULUPAD = p.KULUPAD;
            id.KULUPKONTEJAN = p.KULUPKONTEJAN;
            int addcontrol = db.SaveChanges();
            if (addcontrol >= 1)
            {
                return RedirectToAction("Index", "Kulup");
            }
            return View();
        }
    }
}