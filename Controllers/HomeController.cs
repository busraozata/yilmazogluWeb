using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YilmazOgluProject.Models;

namespace YilmazOgluProject.Controllers
{
    public class HomeController : Controller
    {
        yilmazoglugroupDBEntities db = new yilmazoglugroupDBEntities();
        public ActionResult Anasayfa()
        {
            return View();
        }
        [Route("~/Hakkimizda")]
        public ActionResult Hakkimizda()
        {
            var about = db.About_tbl.Where(q => q.About_ID == 1).FirstOrDefault();
            return View(about);
        }
        [Route("~/Hizmetlerimiz")]
        public ActionResult Hizmetlerimiz()
        {
            var services = db.Services_tbl.ToList();
            return View(services);
     
        }
        [Route("~/HizmetDetay/{ID}")]
        public ActionResult HizmetDetay(String ID)
        {
            var convert = Convert.ToInt32(ID);
            var servicedetail = db.Services_tbl.Where(q=>q.Service_ID == convert).FirstOrDefault();
            return View(servicedetail);

        }
        [Route("~/Projelerimiz")]
        public ActionResult Projelerimiz()
        {
            var projects = db.Projects_tbl.ToList();
            return View(projects);

        }
        [Route("~/ProjeDetay/{ID}")]
        public ActionResult ProjeDetay(String ID)
        {
            var convert = Convert.ToInt32(ID);
            var projectdetail = db.Projects_tbl.Where(q => q.Project_ID == convert).FirstOrDefault();
            return View(projectdetail);

        }
        [Route("~/Haberler")]
        public ActionResult Haberler()
        {
            ViewBag.deny = TempData["Deny"];

            var news = db.News_tbl.ToList();
            return View(news);
     

        }
        [Route("~/HaberDetay/{ID}")]
        public ActionResult HaberDetay(String ID)
        {
            var convert = Convert.ToInt32(ID);
            var newsdetail = db.News_tbl.Where(q => q.Blog_ID == convert).FirstOrDefault();
            return View(newsdetail);

        }
        [Route("~/iletisim")]
        public ActionResult iletisim()
        {
            return View();
        }

 
        [HttpPost]
        public ActionResult PostCV(HttpPostedFileBase Andr)
        {
        
            Random rand = new Random();
            int denemesayi = rand.Next(10000, 99999999);
            String random = denemesayi.ToString();
            string filename = Path.GetFileName(Andr.FileName);
            string extension = Path.GetExtension(filename);
            Cv_tbl add = new Cv_tbl();
            if (Andr != null && Andr.ContentLength > 0 && extension == ".pdf")
            {
                add.Cv_Path = "https://localhost:44308/ResimDosyasi/resimler/" + random + ".png";
                String addpdf = random + ".pdf";
                var path = Path.Combine(Server.MapPath("~/PdfDosyalari/pdfler/"), addpdf);
                Andr.SaveAs(path);
                db.Cv_tbl.Add(add);
                db.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                TempData["Deny"] = "Cv Gönderilemedi Bilgileri Tekrar Kontrol Ediniz..!";
       
                return RedirectToAction("Haberler", "Home");
            }
          
           
        }

        
}
}