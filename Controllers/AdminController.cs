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
    public class AdminController : Controller
    {
        yilmazoglugroupDBEntities db = new yilmazoglugroupDBEntities();


        public ActionResult Login()
        {


            return View();
        }

        [HttpPost]

        public ActionResult Login(Login_tbl t)
        {

            {

                var bilgiler = db.Login_tbl.FirstOrDefault(x => x.Login_Name == t.Login_Name && x.Login_Password == t.Login_Password);
                if (bilgiler != null)
                {

                    var veriler = db.Login_tbl.FirstOrDefault(x => x.Login_Name == t.Login_Name && x.Login_Password == t.Login_Password);

                    if (veriler.Login_ID != 0)
                    {
                        HttpCookie cookieL = new HttpCookie("cerez2", veriler.Login_ID.ToString());
                        cookieL.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(cookieL);

                    }
                    else if (veriler.Login_ID == 0)
                    {
                        HttpCookie cookieL = new HttpCookie("cerez2", veriler.Login_ID.ToString());
                        cookieL.Expires = DateTime.Now.AddHours(1);
                        Response.Cookies.Add(cookieL);

                    }
                    HttpCookie girisid = new HttpCookie("cerez4", veriler.Login_ID.ToString());
                    Response.Cookies.Add(girisid);

                    return RedirectToAction("Index", "Admin");

                }
                else
                {
                    ViewBag.Error = "Hatalı Kullanıcı Adı Veya Şifre";
                    return View("Login");

                }

            }
        }

        //Haberler Bölümü Başlangıç//

        public ActionResult UserManagement()
        {
            var list = db.Login_tbl.ToList();
            return View(list);
        }
        public ActionResult AddUser()
        {
            return View();
        }
   
        [HttpPost]
        public ActionResult AddUserPost(Login_tbl t)
        {
            Login_tbl add = new Login_tbl
            {
                Login_Name = t.Login_Name,
                Login_Password = t.Login_Password,
            };

            db.Login_tbl.Add(add);
            db.SaveChanges();
            return RedirectToAction("UserManagement", "Admin");
        }
      
        [HttpPost]
        public ActionResult DeleteUser(String ID)
        {
            int convert = Convert.ToInt32(ID);
            var add = db.Login_tbl.Where(q => q.Login_ID == convert).FirstOrDefault();
            db.Login_tbl.Remove(add);
            db.SaveChanges();
        
            return RedirectToAction("UserManagement", "Admin");
        }

        //Haberler Bölümü Bitiş//
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //Projeler Bölümü Başlangıç//
        public ActionResult ProjectManagement()
        {
            var list = db.Projects_tbl.ToList();
            return View(list);
        }
        public ActionResult AddProject()
        {
            return View();
        }
        public ActionResult EditProject(String ID)
        {
            int convert = Convert.ToInt32(ID);
            var list = db.Projects_tbl.Where(q=>q.Project_ID == convert).FirstOrDefault();
            return View(list);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddProjectPost(Projects_tbl t, HttpPostedFileBase Andr , HttpPostedFileBase[] Andr2)
        {
            Random rand = new Random();
            int denemesayi = rand.Next(10000, 999999);
            String random = denemesayi.ToString();

            Projects_tbl add = new Projects_tbl
            {
                Project_City = t.Project_City,
                Project_Content = t.Project_Content,
                Project_Content_Two = t.Project_Content_Two,
                Project_Country = t.Project_Country,
                Project_EndDate = t.Project_EndDate,
                Project_Sector = t.Project_Sector,
                Project_Situation = t.Project_Situation,
                Project_Statu = t.Project_Statu,
                Project_Summary = t.Project_Summary,
                Project_Summary_Two = t.Project_Summary_Two,
                Project_StartDate = t.Project_StartDate,
                Project_Header = t.Project_Header,
                Project_Header_Two = t.Project_Header_Two,
                
                
            };
            if (Andr != null && Andr.ContentLength > 0)
            {
                add.Project_Image = "https://localhost:44308/ResimDosyasi/resimler/" + random + ".png";
                String resminkoyulduguyer = random + ".png";
                var path = Path.Combine(Server.MapPath("~/ResimDosyasi/resimler/"), resminkoyulduguyer);
                Andr.SaveAs(path);

            }         
            db.Projects_tbl.Add(add);
            db.SaveChanges();
            if (Andr2 != null)
            {
                foreach(var item in Andr2)
                {
                    Random rand2 = new Random();
                    int denemesayi2 = rand2.Next(1000, 99999999);
                    String random2 = denemesayi2.ToString();
                    AltSlider_tbl addslider = new AltSlider_tbl();
                    addslider.AltSlider_Project_ID = add.Project_ID;
                    addslider.AltSlider_Path = "https://localhost:44308/ResimDosyasi/resimler/" + random2 + ".png";
                    String resminkoyulduguyer2 = random2 + ".png";
                    var path2 = Path.Combine(Server.MapPath("~/ResimDosyasi/resimler/"), resminkoyulduguyer2);
                    item.SaveAs(path2);
                    db.AltSlider_tbl.Add(addslider);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("ProjectManagement", "Admin");
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditProjectPost(Projects_tbl t, HttpPostedFileBase Andr, String ID)
        {
            int convert = Convert.ToInt32(ID);
            Random rand = new Random();
            int denemesayi = rand.Next(10000, 999999);
            String random = denemesayi.ToString();
            var edit = db.Projects_tbl.Where(q=>q.Project_ID == convert).FirstOrDefault();
            edit.Project_City = t.Project_City;
            edit.Project_Content = t.Project_Content;
            edit.Project_Content_Two = t.Project_Content_Two;
            edit.Project_Country = t.Project_Country; 
            edit.Project_Sector = t.Project_Sector;
            edit.Project_Situation = t.Project_Situation;
            edit.Project_Statu = t.Project_Statu;
            edit.Project_Summary = t.Project_Summary;
            edit.Project_Summary_Two = t.Project_Summary_Two;
            if(t.Project_StartDate != null)
            {
                edit.Project_StartDate = t.Project_StartDate;
            }
            if(t.Project_EndDate != null)
            {
                edit.Project_EndDate = t.Project_EndDate;
            }         
            edit.Project_Header = t.Project_Header;
            edit.Project_Header_Two = t.Project_Header_Two;
            if (Andr != null && Andr.ContentLength > 0)
            {
                edit.Project_Image = "https://localhost:44308/ResimDosyasi/resimler/" + random + ".png";
                String resminkoyulduguyer = random + ".png";
                var path = Path.Combine(Server.MapPath("~/ResimDosyasi/resimler/"), resminkoyulduguyer);
                Andr.SaveAs(path);
            }
            db.SaveChanges();
            return RedirectToAction("ProjectManagement", "Admin");
        }
        [HttpPost]
        public ActionResult DeleteProject(String ID)
        {
            int convert = Convert.ToInt32(ID);
            var add = db.Projects_tbl.Where(q => q.Project_ID == convert).FirstOrDefault();
            db.Projects_tbl.Remove(add);
            db.SaveChanges();
            TempData["ID"] = "İşleminiz Başarılı..!";
            return RedirectToAction("ProjectManagement", "Admin");
        }


        //Projeler Bölümü Bitiş//

        //Hizmetler Bölümü Başlangıç//

        public ActionResult ServiceManagement()
        {
            var list = db.Services_tbl.ToList();
            return View(list);
        }
        public ActionResult AddService()
        {
            return View();
        }
        public ActionResult EditService(String ID)
        {
            int convert = Convert.ToInt32(ID);
            var list = db.Services_tbl.Where(q => q.Service_ID == convert).FirstOrDefault();
            return View(list);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddServicePost(Services_tbl t, HttpPostedFileBase Andr, HttpPostedFileBase[] Andr2, HttpPostedFileBase[] Andr3)
        {
            Random rand = new Random();
            int denemesayi = rand.Next(10000, 999999);
            String random = denemesayi.ToString();

            Services_tbl add = new Services_tbl
            {
                Service_Content = t.Service_Content,
                Service_Content_Two = t.Service_Content_Two,
                Service_Header = t.Service_Header,
                Service_Header_Two = t.Service_Header_Two,
                Service_Summary = t.Service_Summary,
                Service_Summary_Two = t.Service_Summary_Two,

            };
            if (Andr != null && Andr.ContentLength > 0)
            {
                add.Service_Image = "https://localhost:44308/ResimDosyasi/resimler/" + random + ".png";
                String resminkoyulduguyer = random + ".png";
                var path = Path.Combine(Server.MapPath("~/ResimDosyasi/resimler/"), resminkoyulduguyer);
                Andr.SaveAs(path);

            }
            db.Services_tbl.Add(add);
            db.SaveChanges();
            if (Andr2 != null)
            {
                foreach (var item in Andr2)
                {
                    Random rand2 = new Random();
                    int denemesayi2 = rand2.Next(1000, 99999999);
                    String random2 = denemesayi2.ToString();
                    AltSlider_tbl addslider = new AltSlider_tbl();
                    addslider.AltSlider_Service_ID = add.Service_ID;
                    addslider.AltSlider_Path = "https://localhost:44308/ResimDosyasi/resimler/" + random2 + ".png";
                    String resminkoyulduguyer2 = random2 + ".png";
                    var path2 = Path.Combine(Server.MapPath("~/ResimDosyasi/resimler/"), resminkoyulduguyer2);
                    item.SaveAs(path2);
                    db.AltSlider_tbl.Add(addslider);
                    db.SaveChanges();
                }
            }
            if (Andr3 != null)
            {
                foreach (var item2 in Andr3)
                {
                    Random rand3= new Random();
                    int denemesayi3 = rand3.Next(1000, 99999999);
                    String random3 = denemesayi3.ToString();
                    Sliders_tbl slider = new Sliders_tbl();
                    slider.Slider_Service_ID = add.Service_ID;
                    slider.Slider_Path = "https://localhost:44308/ResimDosyasi/resimler/" + random3 + ".png";
                    String resminkoyulduguyer3 = random3 + ".png";
                    var path3= Path.Combine(Server.MapPath("~/ResimDosyasi/resimler/"), resminkoyulduguyer3);
                    item2.SaveAs(path3);
                    db.Sliders_tbl.Add(slider);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("ServiceManagement", "Admin");
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditServicePost(Services_tbl t, HttpPostedFileBase Andr, String ID)
        {
            int convert = Convert.ToInt32(ID);
            Random rand = new Random();
            int denemesayi = rand.Next(10000, 999999);
            String random = denemesayi.ToString();

            var edit = db.Services_tbl.Where(q => q.Service_ID == convert).FirstOrDefault();
            edit.Service_Content = t.Service_Content;
            edit.Service_Content_Two = t.Service_Content_Two;
            edit.Service_Header = t.Service_Header;
            edit.Service_Header_Two = t.Service_Header_Two;
            edit.Service_Summary = t.Service_Summary;
            edit.Service_Summary_Two = t.Service_Summary_Two;
     
            if (Andr != null && Andr.ContentLength > 0)
            {
                edit.Service_Image = "https://localhost:44308/ResimDosyasi/resimler/" + random + ".png";
                String resminkoyulduguyer = random + ".png";
                var path = Path.Combine(Server.MapPath("~/ResimDosyasi/resimler/"), resminkoyulduguyer);
                Andr.SaveAs(path);

            }
            db.SaveChanges();
            return RedirectToAction("ServiceManagement", "Admin");
        }
        [HttpPost]
        public ActionResult DeleteService(String ID)
        {
            int convert = Convert.ToInt32(ID);
            var add = db.Services_tbl.Where(q => q.Service_ID == convert).FirstOrDefault();
            db.Services_tbl.Remove(add);
            db.SaveChanges();
            TempData["ID"] = "İşleminiz Başarılı..!";
            return RedirectToAction("ServiceManagement", "Admin");
        }

        //Hizmetler Bölümü Bitiş//



        //Haberler Bölümü Başlangıç//

        public ActionResult NewsManagement()
        {
            var list = db.News_tbl.ToList();
            return View(list);
        }
        public ActionResult AddNews()
        {
            return View();
        }
        public ActionResult EditNews(String ID)
        {
            int convert = Convert.ToInt32(ID);
            var list = db.News_tbl.Where(q => q.Blog_ID == convert).FirstOrDefault();
            return View(list);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddNewsPost(News_tbl t, HttpPostedFileBase Andr)
        {
            Random rand = new Random();
            int denemesayi = rand.Next(10000, 999999);
            String random = denemesayi.ToString();

            News_tbl add = new News_tbl
            {
                Blog_Content = t.Blog_Content,
                Blog_Content_Two = t.Blog_Content_Two,
                Blog_Header = t.Blog_Header,
                Blog_Header_Two = t.Blog_Header_Two,
                Blog_Summary = t.Blog_Summary,
                Blog_Summary_Two = t.Blog_Summary_Two,
                Blog_AddDate = DateTime.Now,

            };
            if (Andr != null && Andr.ContentLength > 0)
            {
                add.Blog_Image = "https://localhost:44308/ResimDosyasi/resimler/" + random + ".png";
                String resminkoyulduguyer = random + ".png";
                var path = Path.Combine(Server.MapPath("~/ResimDosyasi/resimler/"), resminkoyulduguyer);
                Andr.SaveAs(path);

            }
            db.News_tbl.Add(add);
            db.SaveChanges();
            return RedirectToAction("NewsManagement", "Admin");
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditNewsPost(News_tbl t, HttpPostedFileBase Andr, String ID)
        {
            int convert = Convert.ToInt32(ID);
            Random rand = new Random();
            int denemesayi = rand.Next(10000, 999999);
            String random = denemesayi.ToString();

            var edit = db.News_tbl.Where(q => q.Blog_ID == convert).FirstOrDefault();
            edit.Blog_Content = t.Blog_Content;
            edit.Blog_Content_Two = t.Blog_Content_Two;
            edit.Blog_Header = t.Blog_Header;
            edit.Blog_Header_Two = t.Blog_Header_Two;
            edit.Blog_Header = t.Blog_Header;
            edit.Blog_Header_Two = t.Blog_Header_Two;
            if (Andr != null && Andr.ContentLength > 0)
            {
                edit.Blog_Image = "https://localhost:44308/ResimDosyasi/resimler/" + random + ".png";
                String resminkoyulduguyer = random + ".png";
                var path = Path.Combine(Server.MapPath("~/ResimDosyasi/resimler/"), resminkoyulduguyer);
                Andr.SaveAs(path);

            }
            db.SaveChanges();
            return RedirectToAction("NewsManagement", "Admin");
        }
        [HttpPost]
        public ActionResult DeleteNews(String ID)
        {
            int convert = Convert.ToInt32(ID);
            var add = db.News_tbl.Where(q => q.Blog_ID == convert).FirstOrDefault();
            db.News_tbl.Remove(add);
            db.SaveChanges();
            TempData["ID"] = "İşleminiz Başarılı..!";
            return RedirectToAction("NewsManagement", "Admin");
        }

        //Haberler Bölümü Bitiş//


        //Markalar Bölümü Başlangıç//

        public ActionResult BrandManagement()
        {
            var list = db.Brands_tbl.ToList();
            return View(list);
        }
        public ActionResult AddBrand()
        {
            return View();
        }
        public ActionResult EditBrand(String ID)
        {
            int convert = Convert.ToInt32(ID);
            var list = db.Brands_tbl.Where(q => q.Brand_ID == convert).FirstOrDefault();
            return View(list);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddBrandPost(Brands_tbl t, HttpPostedFileBase Andr)
        {
            Random rand = new Random();
            int denemesayi = rand.Next(10000, 999999);
            String random = denemesayi.ToString();

            Brands_tbl add = new Brands_tbl
            {
                Brand_Content = t.Brand_Content,
                Brand_Content_Two = t.Brand_Content_Two,

            };
            if (Andr != null && Andr.ContentLength > 0)
            {
                add.Brand_Image = "https://localhost:44308/ResimDosyasi/resimler/" + random + ".png";
                String resminkoyulduguyer = random + ".png";
                var path = Path.Combine(Server.MapPath("~/ResimDosyasi/resimler/"), resminkoyulduguyer);
                Andr.SaveAs(path);

            }
            db.Brands_tbl.Add(add);
            db.SaveChanges();
            return RedirectToAction("BrandManagement", "Admin");
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditBrandPost(News_tbl t, HttpPostedFileBase Andr, String ID)
        {
            int convert = Convert.ToInt32(ID);
            Random rand = new Random();
            int denemesayi = rand.Next(10000, 999999);
            String random = denemesayi.ToString();

            var edit = db.Brands_tbl.Where(q => q.Brand_ID == convert).FirstOrDefault();
            edit.Brand_Content = t.Blog_Content;
            edit.Brand_Content_Two = t.Blog_Content_Two;
            if (Andr != null && Andr.ContentLength > 0)
            {
                edit.Brand_Image = "https://localhost:44308/ResimDosyasi/resimler/" + random + ".png";
                String resminkoyulduguyer = random + ".png";
                var path = Path.Combine(Server.MapPath("~/ResimDosyasi/resimler/"), resminkoyulduguyer);
                Andr.SaveAs(path);

            }
            db.SaveChanges();
            return RedirectToAction("BrandManagement", "Admin");
        }
        [HttpPost]
        public ActionResult DeleteBrand(String ID)
        {
            int convert = Convert.ToInt32(ID);
            var add = db.Brands_tbl.Where(q => q.Brand_ID == convert).FirstOrDefault();
            db.Brands_tbl.Remove(add);
            db.SaveChanges();
            TempData["ID"] = "İşleminiz Başarılı..!";
            return RedirectToAction("BrandManagement", "Admin");
        }

        //Markalar Bölümü Bitiş//

        //Ayarlar Bölümü Başlangıç//

        public ActionResult Settings()
        {
            var list = db.Settings_tbl.Where(q => q.Settings_ID == 1).FirstOrDefault();
            return View(list);
        }

        [HttpPost]
        public ActionResult EditSettingsPost(Settings_tbl t)
        {

            var edit = db.Settings_tbl.Where(q => q.Settings_ID == 1).FirstOrDefault();
            edit.Settings_Address = t.Settings_Address;
            edit.Settings_Facebook = t.Settings_Facebook;
            edit.Settings_Phone = t.Settings_Phone;
            edit.Settings_Phone_Mobile = t.Settings_Phone_Mobile;
            edit.Settings_Mail = t.Settings_Mail;
            edit.Settings_Twitter = t.Settings_Twitter;
            edit.Settings_Instagram = t.Settings_Instagram;
            db.SaveChanges();
            return RedirectToAction("Settings", "Admin");
        }

        //Ayarlar Bölümü Bitiş//
    }
}