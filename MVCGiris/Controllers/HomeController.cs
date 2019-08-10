using MVCGiris.Filters;
using MVCGiris.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCGiris.Controllers
{

    public class HomeController : Controller
    {
        Dictionary<string, object> dictList = new Dictionary<string, object>();


        SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ToString());
        List<IndexModel> a = new List<IndexModel>();
        List<HaberModel> x = new List<HaberModel>();
        List<KategoriModel> c = new List<KategoriModel>();
        MetinKısalt metinKısalt = new MetinKısalt();

        OrtakModel om = new OrtakModel();
        // GET: Home

        public (int toplam, int carpim) ToplamCarpim(int x, int y, int z)
        {
            var t = x + y + z;
            var c = x * y * z;
            return (t, c);
        }

        public int Toplam(int x, int y, int z, out decimal carpim, ref decimal bolum)
        {
            var t = x + y + z;

            carpim = x * y * z;

            bolum = x / y / z;

            return t;
        }
        public int deneme()
        {
            int a = 123;
            return a;
        }

        public ActionResult Index()
        {

            //BaglantiAc();
            //string ySorgu = "SELECT COUNT(*) from haberler0";
            //SqlCommand yCmd = new SqlCommand(ySorgu, cnn);
            //SqlDataReader dr = yCmd.ExecuteReader();
            //dr.Read();
            //TempData["Counts"] = dr[0].ToString();
            //cnn.Close();
            //return View();
            using (AdoHelper adoHelper = new AdoHelper())
            {
                string sorgu = "SELECT *  FROM  haberler0 where aktif=@aktif";
                dictList.Clear();
                dictList.Add("@aktif", 1);
                SqlDataReader oku = adoHelper.ExecDataReaderProc(sorgu, dictList);
                List<HaberModel> Listele = new List<HaberModel>();
                //var dd = adoHelper.GetDefault<string>();

                //dd.GetType();

                while (oku.Read())
                {
                    var veri = new HaberModel();
                    veri.id = oku["id"].ToString();
                    veri.resim = oku["img"].ToString();
                    veri.baslik = metinKısalt.baslik(oku["baslik"].ToString());
                    veri.icerik = metinKısalt.icerik(oku["icerik"].ToString());
                    veri.kategori = oku["kategori"].ToString();
                    veri.tarih = oku["tarih"].ToString();
                    x.Add(veri);
                }
            }
            return View(x);
            //x.Clear();
            //string sorgu = "SELECT *  FROM  haberler0 where aktif=1";
            //SqlCommand cmd = new SqlCommand(sorgu, cnn);
            //SqlDataReader oku = cmd.ExecuteReader();
            //List<HaberModel> Listele = new List<HaberModel>();
            //while (oku.Read())
            //{
            //    var veri = new HaberModel();
            //    veri.id = oku["id"].ToString();
            //    veri.resim = oku["img"].ToString();
            //    veri.baslik = metinKısalt.baslik(oku["baslik"].ToString());
            //    veri.icerik = metinKısalt.icerik(oku["icerik"].ToString());
            //    veri.kategori = oku["kategori"].ToString();
            //    veri.tarih = oku["tarih"].ToString();
            //    x.Add(veri);
            //}
            //cnn.Close();
            //return View(x);

            //Information ınformation = new Information();
            //ınformation.Title = "asd";
            
        }

        public JsonResult JsonIndex(int? page, int? count)
        {
            using (AdoHelper adoHelper = new AdoHelper())
            {
                string ysorgu = "SELECT COUNT(*) from haberler0";
                dictList.Clear();
                SqlDataReader dr = adoHelper.ExecDataReaderProc(ysorgu, dictList);
                dr.Read();
                TempData["Counts"] = dr[0].ToString();
            }
            //    BaglantiAc();
            //string ySorgu = "SELECT COUNT(*) from haberler0";
            //SqlCommand yCmd = new SqlCommand(ySorgu, cnn);
            //SqlDataReader dr = yCmd.ExecuteReader();
            //dr.Read();
            //TempData["Counts"] = dr[0].ToString();
            //cnn.Close();

            BaglantiAc();
            int PageNo = Convert.ToInt32(page - 1);
            //var y = Session["AdSoyad"];
            x.Clear();
            string sorgu = "SELECT *  FROM  haberler0 where aktif=1 ORDER BY id  OFFSET @start ROWS FETCH NEXT @finish ROWS ONLY";
            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            cmd.Parameters.AddWithValue("@start", PageNo * count);
            cmd.Parameters.AddWithValue("@finish", count);
            SqlDataReader oku = cmd.ExecuteReader();
            List<HaberModel> Listele = new List<HaberModel>();
            while (oku.Read())
            {
                var veri = new HaberModel();
                veri.id = oku["id"].ToString();
                veri.baslik = oku["baslik"].ToString();
                veri.icerik = metinKısalt.icerik(oku["icerik"].ToString());
                veri.kategori = oku["kategori"].ToString();
                veri.tarih = oku["tarih"].ToString();
                Listele.Add(veri);
            }
            cnn.Close();
            return Json(Listele, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PartialBildirim()
        {
            return View();

        }
        List<KategoriModel> ModelKategori = new List<KategoriModel>();
        public ActionResult KategoriListele()
        {
            ModelKategori.Clear();
            using (AdoHelper adoHelper = new AdoHelper())
            {
                string sorgu = "Select * From kategori";
                dictList.Clear();
                SqlDataReader oku = adoHelper.ExecDataReaderProc(sorgu, dictList);
                while (oku.Read())
                {
                    var veri = new KategoriModel();
                    veri.KategoriId = Convert.ToInt32(oku["id"]);
                    veri.Kategori = oku["kategori"].ToString();
                    ModelKategori.Add(veri);
                }

                return View(ModelKategori);

            }
            //    BaglantiAc();
            //string sorgu = "Select * From kategori";
            //SqlCommand cmd = new SqlCommand(sorgu, cnn);
            //SqlDataReader oku = cmd.ExecuteReader();
            //while (oku.Read())
            //{
            //    var veri = new KategoriModel();
            //    veri.KategoriId = Convert.ToInt32(oku["id"]);
            //    veri.Kategori = oku["kategori"].ToString();
            //    ModelKategori.Add(veri);
            //}

            //cnn.Close();
            //return View(ModelKategori);

        }

        [AutFilter]
        public ActionResult KategoriSil(int? id)
        {
            using (AdoHelper adoHelper = new AdoHelper())
            {
                dictList.Clear();
                string sorgu = "delete FROM kategori where id=@id";
                dictList.Add("@id", id.ToString());
                adoHelper.ExecNonQueryProc(sorgu, dictList);
                return RedirectToAction("KategoriListele");
            }
        }


        [AllowAnonymous]
        public JsonResult JsonMesssage()
        {
            if (Session["id"] == null) { return Json(null, JsonRequestBehavior.AllowGet); }
            using (AdoHelper adoHelper = new AdoHelper())
            {
                string sorgu = "Select Title,id From Messages Where GetID=@getid AND Okundu=0 ORDER BY Tarih";
                dictList.Clear();
                dictList.Add("@getid", Session["id"]);
                SqlDataReader oku = adoHelper.ExecDataReaderProc(sorgu, dictList);
                List<MesajModel> Liste = new List<MesajModel>();
                try
                {
                    while (oku.Read())
                    {
                        MesajModel jsonm = new MesajModel();
                        jsonm.baslik = oku["Title"].ToString();
                        jsonm.id = oku["id"].ToString();
                        //new JsonMessage { baslik = oku["Title"].ToString(), mesaj = oku["Message"].ToString() };
                        Liste.Add(jsonm);
                    }
                }


                catch (Exception e)
                {

                    throw;
                }

                return Json(Liste, JsonRequestBehavior.AllowGet);

            }
            //    cnn.Open();
            //string sorgu = "Select Title,id From Messages Where GetID=@getid AND Okundu=0 ORDER BY Tarih";
            //SqlCommand cmd = new SqlCommand(sorgu, cnn);
            //cmd.Parameters.AddWithValue("@getid", Session["id"]);
            //SqlDataReader oku = cmd.ExecuteReader();

            //List<MesajModel> Liste = new List<MesajModel>();
            //try
            //{
            //    while (oku.Read())
            //    {
            //        MesajModel jsonm = new MesajModel();
            //        jsonm.baslik = oku["Title"].ToString();
            //        jsonm.id = oku["id"].ToString();
            //        //new JsonMessage { baslik = oku["Title"].ToString(), mesaj = oku["Message"].ToString() };
            //        Liste.Add(jsonm);
            //    }
            //}


            //catch (Exception e)
            //{

            //    throw;
            //}

            //cnn.Close();
            //return Json(Liste, JsonRequestBehavior.AllowGet);
        }

        public ActionResult login()
        {
            Session.RemoveAll();
            return View();
        }

        [HttpPost]
        public ActionResult login(LoginModel model)
        {
            if (!ModelState.IsValid) { return View(); }
            int tur;
            using (AdoHelper adoHelper = new AdoHelper())
            {
                string sorgu = "Select id,userName,userClass From users where email=@k and userPw=@s";
                dictList.Clear();
                dictList.Add("@k", model.Mail);
                dictList.Add("@s", model.Password);
                SqlDataReader oku = adoHelper.ExecDataReaderProc(sorgu, dictList);
                adoHelper.GetDefault<LoginModel>();
                if (oku.Read())
                {
                    Session["id"] = oku[0];
                    Session["AdSoyad"] = oku[1];
                    tur = int.Parse(oku[2].ToString());
                    Session["login"] = tur;
                    if (tur == 1)
                    {
                        Session["login"] = "admin";
                    }
                    else
                    {
                        Session["login"] = "kullanici";
                    }

                    return RedirectToAction("Index", "Home");


                }
                else
                {

                    ViewBag.mesaj = "Başarısız";
                    return View();
                }
            }
        }

        public ActionResult UpdatePw()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePw(UpdatePwModel model)
        {
            if (Session["id"] != null)
            {
                using (AdoHelper adoHelper = new AdoHelper())
                {
                    dictList.Clear();
                    string sorgu = "Update users SET userPw=@NewPw Where id=@id AND userPw=@userPw";
                    dictList.Add("@id", Convert.ToInt32(Session["id"]));
                    dictList.Add("@userPw", model.OldPw);
                    dictList.Add("@NewPw", model.Pw);
                    adoHelper.ExecNonQueryProc(sorgu, dictList);
                }
            }

            return View();
        }

            public ActionResult register()
            {
                return View();
            }

        [HttpPost]
        public ActionResult register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {

                return View();

            }
            else
            {

                using (AdoHelper adoHelper = new AdoHelper())
                {
                    dictList.Clear();
                    string sorgu = "insert into users (userName,userPw,userClass,cDate,aktif,email) Values (@userName,@userPw,@userClass,@cDate,@aktif,@email)";
                    dictList.Add("@userName", model.Ad + " " + model.Soyad);
                    dictList.Add("@userPw", model.Sifre);
                    dictList.Add("@userClass", "0");
                    dictList.Add("@cDate", DateTime.Now);
                    dictList.Add("@aktif", "1");
                    dictList.Add("@email", model.Email);
                    adoHelper.ExecNonQueryProc(sorgu, dictList);
                    ViewBag.mesaj = "Kayıt başarılı";
                    return RedirectToAction("Index", "Home");
                }

                //BaglantiAc();
                //string sorgu = "insert into users (userName,userPw,userClass,cDate,aktif,email) Values (@userName,@userPw,@userClass,@cDate,@aktif,@email)";
                //SqlCommand cmd = new SqlCommand(sorgu, cnn);
                //cmd.Parameters.AddWithValue("@userName", model.Ad + " " + model.Soyad);
                //cmd.Parameters.AddWithValue("@userPw", model.Sifre);
                //cmd.Parameters.AddWithValue("@userClass", "0");
                //cmd.Parameters.AddWithValue("@cDate", DateTime.Now);
                //cmd.Parameters.AddWithValue("@aktif", "1");
                //cmd.Parameters.AddWithValue("@email", model.Email);
                //cmd.ExecuteNonQuery();
                //cnn.Close();
            }

        }

        public ActionResult DizaynAdmin()
        {
            return View();
        }

        public ActionResult DizaynKullanici()
        {
            return View();
        }

        [AutFilter]
        public ActionResult HaberEkle(int? id)
        {
            if (id != null)
            {
                HaberModel b = new HaberModel();
                BaglantiAc();
                string sorgu = "Select * From haberler0 Where id=@id";
                SqlCommand cmd = new SqlCommand(sorgu, cnn);
                cmd.Parameters.Add("@id", Convert.ToInt32(id));
                SqlDataReader oku = cmd.ExecuteReader();
                oku.Read();
                b.baslik = oku["baslik"].ToString();
                b.icerik = metinKısalt.icerik(oku["icerik"].ToString());
                cnn.Close();
                FillDropdown();
                return View(b);
            }
            else
            {
                FillDropdown();
                return View();
            }


        }
        [AutFilter]
        public ActionResult HaberEkleGuncelle(string sorgu, string baslik, string icerik, string kategori, int id)
        {
            //string sorgu = "Update haberler0 SET baslik=@baslik,icerik=@icerik,kategori=@kategori Where id=@id";
            string query = sorgu;
            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            cnn.Open();
            cmd.Parameters.AddWithValue("@baslik", baslik);
            cmd.Parameters.AddWithValue("@icerik", icerik);
            cmd.Parameters.AddWithValue("@kategori", kategori);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cnn.Close();
            return View();

        }
        [HttpPost]
        [AutFilter]
        public ActionResult HaberEkle(HaberModel model, int? id, HttpPostedFileBase file)
        {
            FillDropdown();
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (id != null)
                {
                    string sorgu = "Update haberler0 SET baslik=@baslik,icerik=@icerik,kategori=@kategori Where id=@id";
                    return HaberEkleGuncelle(sorgu, model.baslik, model.icerik, model.kategori, Convert.ToInt32(id));

                }
                else
                {
                    file.SaveAs(Server.MapPath("/image/" + file.FileName));
                    string yol = "/image/" + file.FileName.ToString();
                    int aktif = 0;
                    if (Session["login"].ToString() == "admin") aktif = 1;
                    AdoHelper adoHelper = new AdoHelper();
                    dictList.Clear();
                    string sorgu = "Insert into haberler0 (img,baslik,icerik,kategori,aktif,tarih,userId) Values (@img,@baslik,@icerik,@kategori,@aktif,@tarih,@userId)";
                    dictList.Add("@img", yol);
                    dictList.Add("@baslik", model.baslik);
                    dictList.Add("@icerik", model.icerik);
                    dictList.Add("@kategori", model.kategori);
                    dictList.Add("@aktif", aktif);
                    dictList.Add("@tarih", DateTime.Now);
                    dictList.Add("@userId", Session["id"].ToString());
                    adoHelper.ExecNonQueryProc(sorgu, dictList);
                    return RedirectToAction("Index", "Home");

                }

            }
        }
        List<MesajModel> p = new List<MesajModel>();

        //public ActionResult deneme()
        //{
        //    return View();
        //}

        public ActionResult HaberGoruntule(int? id)
        {
            BaglantiAc();
            var veri = new HaberModel();
            string sorgu = "SELECT *  FROM  haberler0 where id=@id";
            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader oku = cmd.ExecuteReader();
            List<HaberModel> Listele = new List<HaberModel>();
            while (oku.Read())
            {
                veri.id = oku["id"].ToString();
                veri.resim = oku["img"].ToString();
                veri.baslik = oku["baslik"].ToString();
                veri.icerik = oku["icerik"].ToString();
                veri.kategori = oku["kategori"].ToString();
                veri.tarih = oku["tarih"].ToString();

            }
            cnn.Close();
            return View(veri);
        }


        [AutFilter]
        public ActionResult Haberlerim()
        {
            BaglantiAc();
            string sorgu = "Select * From haberler0 where userId=@k";
            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            cmd.Parameters.Add("@k", Session["id"].ToString());
            SqlDataReader oku = cmd.ExecuteReader();

            while (oku.Read())
            {
                var veri = new HaberModel();
                veri.id = oku["id"].ToString();
                veri.baslik = oku["baslik"].ToString();
                veri.icerik = metinKısalt.icerik(oku["icerik"].ToString());
                veri.kategori = oku["kategori"].ToString();
                veri.tarih = oku["tarih"].ToString();
                x.Add(veri);
            }

            cnn.Close();
            return View(x);
        }

        [AutFilter]
        public ActionResult OnayBekleyen()
        {
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ToString());
            cnn.Open();
            string sorgu = "Select * From haberler0 Where aktif=0";
            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            SqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                var veri = new HaberModel();
                veri.id = oku["id"].ToString();
                veri.baslik = oku["baslik"].ToString();
                veri.icerik = oku["icerik"].ToString();
                veri.kategori = oku["kategori"].ToString();
                veri.tarih = oku["tarih"].ToString();
                x.Add(veri);
            }

            cnn.Close();
            return View(x);
        }
        //[HttpPost]
        //public ActionResult OnayBekleyen()
        //{

        //    return View();
        //}

        [AutFilter]
        public ActionResult Onayla(int? id)
        {
            using (AdoHelper adoHelper = new AdoHelper())
            {
                dictList.Clear();
                string sorgu = "update haberler0 set aktif=@aktif where id=@id";
                dictList.Add("@aktif", 1);
                dictList.Add("@id", id.ToString());
                adoHelper.ExecNonQueryProc(sorgu, dictList);
                return RedirectToAction("OnayBekleyen");
            }

        }

        [AutFilter]
        public ActionResult Sil(int? id)
        {
            using (AdoHelper adoHelper = new AdoHelper())
            {
                dictList.Clear();
                string sorgu = "delete FROM haberler0 where id=@id";
                dictList.Add("@id", Convert.ToInt32(id));
                adoHelper.ExecNonQueryProc(sorgu, dictList);
            }
            return RedirectToAction("OnayBekleyen");
        }

        [AutFilter]
        public ActionResult TumHaberler()
        {
            a.Clear();
            BaglantiAc();
            string sorgu = "Select * From haberler0";
            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            SqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                var veri = new HaberModel();
                veri.id = oku["id"].ToString();
                veri.baslik = oku["baslik"].ToString();
                veri.icerik = oku["icerik"].ToString();
                veri.kategori = oku["kategori"].ToString();
                veri.aktif = oku["aktif"].ToString();
                veri.tarih = oku["tarih"].ToString();
                x.Add(veri);
            }

            cnn.Close();
            return View(x);
        }

        [AutFilter]
        public ActionResult Aktif(int? id)
        {
            using (AdoHelper adoHelper = new AdoHelper())
            {
                dictList.Clear();
                string sorgu = "update haberler0 set aktif=@aktif where id=@id";
                dictList.Add("@aktif", 1);
                dictList.Add("@id", id.ToString());
                adoHelper.ExecNonQueryProc(sorgu, dictList);
                return RedirectToAction("TumHaberler");

            }

        }





        [AutFilter]
        public ActionResult Pasif(int? id)
        {
            using (AdoHelper adoHelper = new AdoHelper())
            {
                dictList.Clear();
                string sorgu = "update haberler0 set aktif=@aktif where id=@id";
                dictList.Add("@aktif", 0);
                dictList.Add("@id", id.ToString());
                adoHelper.ExecNonQueryProc(sorgu, dictList);
                return RedirectToAction("TumHaberler");

            }
        }

        [AutFilter]
        public ActionResult Sil0(int? id)
        {
            using (AdoHelper adoHelper = new AdoHelper())
            {
                dictList.Clear();
                string sorgu = "delete FROM haberler0 where id=@id";
                dictList.Add("@id", Convert.ToInt32(id));
                adoHelper.ExecNonQueryProc(sorgu, dictList);
            }

            return RedirectToAction("TumHaberler");
        }

        [AutFilter]
        public ActionResult Sil1(int? id)
        {
            using (AdoHelper adoHelper = new AdoHelper())
            {
                dictList.Clear();
                string sorgu = "delete FROM haberler0 where id=@id";
                dictList.Add("@id", Convert.ToInt32(id));
                adoHelper.ExecNonQueryProc(sorgu, dictList);
            }
            return RedirectToAction("Haberlerim");

        }

        [AutFilter]
        public ActionResult KullaniciAktifPasif()
        {
            a.Clear();
            using (AdoHelper adoHelper = new AdoHelper())
            {
                string sorgu = "Select * From users";
                dictList.Clear();
                SqlDataReader oku = adoHelper.ExecDataReaderProc(sorgu, dictList);
                while (oku.Read())
                {
                    var veri = new IndexModel();
                    veri.id = oku["id"].ToString();
                    veri.userName = oku["userName"].ToString();
                    veri.userPw = oku["userPw"].ToString();
                    veri.email = oku["email"].ToString();
                    veri.cDate = oku["cDate"].ToString();
                    veri.aktif = oku["aktif"].ToString();
                    a.Add(veri);
                }

                return View(a);
            }




            //    BaglantiAc();
            //string sorgu = "Select * From users";
            //SqlCommand cmd = new SqlCommand(sorgu, cnn);
            //SqlDataReader oku = cmd.ExecuteReader();
            //while (oku.Read())
            //{
            //    var veri = new IndexModel();
            //    veri.id = oku["id"].ToString();
            //    veri.userName = oku["userName"].ToString();
            //    veri.userPw = oku["userPw"].ToString();
            //    veri.email = oku["email"].ToString();
            //    veri.cDate = oku["cDate"].ToString();
            //    veri.aktif = oku["aktif"].ToString();
            //    a.Add(veri);
            //}

            //cnn.Close();
            //return View(a);

        }

        [AutFilter]
        public ActionResult Aktif3(int? id)
        {
            using (AdoHelper adoHelper = new AdoHelper())
            {
                dictList.Clear();
                string sorgu = "update users set aktif=@aktif where id=@id";
                dictList.Add("@id", id.ToString());
                dictList.Add("@aktif", 1);
                adoHelper.ExecNonQueryProc(sorgu, dictList);
                return RedirectToAction("KullaniciAktifPasif");

            }
        }

        [AutFilter]
        public ActionResult Pasif3(int? id)
        {
            using (AdoHelper adoHelper = new AdoHelper())
            {
                dictList.Clear();
                string sorgu = "update users set aktif=@aktif where id=@id";
                dictList.Add("@id", id.ToString());
                dictList.Add("@aktif", 0);
                adoHelper.ExecNonQueryProc(sorgu, dictList);
                return RedirectToAction("KullaniciAktifPasif");
            }
        }

        [AutFilter]
        public ActionResult Sil2(int? id)
        {
            using (AdoHelper adoHelper = new AdoHelper())
            {
                dictList.Clear();
                string sorgu = "delete FROM users where id=@id";
                dictList.Add("@id", id.ToString());
                adoHelper.ExecNonQueryProc(sorgu, dictList);
                return RedirectToAction("KullaniciAktifPasif");
            }
        }

        public ActionResult KategoriEkle()
        {
            return View();
        }


        [HttpPost]
        public ActionResult KategoriEkle(KategoriModel model)
        {
            using (AdoHelper adoHelper = new AdoHelper())
            {
                dictList.Clear();
                string sorgu = "insert into kategori (kategori) Values (@kategori)";
                dictList.Add("@kategori", model.Kategori);
                adoHelper.ExecNonQueryProc(sorgu, dictList);
                return RedirectToAction("Index");
            }

        }

        private void HaberSil(int v)
        {
            using (AdoHelper adoHelper = new AdoHelper())
            {
                dictList.Clear();
                string sorgu = "delete FROM haberler0 where id=@id";
                dictList.Add("@id", v.ToString());
                adoHelper.ExecNonQueryProc(sorgu, dictList);
            }
        }

        private void BaglantiAc()
        {

            cnn.Open();

        }

        public SelectList FillDropdown()
        {
            cnn.Open();
            List<SelectListItem> kategoriler = new List<SelectListItem>();

            string sorgu = "Select id,kategori From kategori";
            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            SqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                kategoriler.Add(new SelectListItem { Text = oku.GetString(1), Value = oku.GetString(1), Selected = false });
            }
            ViewBag.kategori = new SelectList(kategoriler.ToList(), "Value", "Text");

            cnn.Close();
            return ViewBag.kategori;
        }
    }
}