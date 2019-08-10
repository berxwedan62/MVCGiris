using MVCGiris.Filters;
using MVCGiris.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCGiris.Controllers
{
    public class MessageController : Controller
    {
        SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["baglanti"].ToString());

        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Message()
        {
            cnn.Open();
            List<SelectListItem> alicilar = new List<SelectListItem>();
            string sorgu = "Select id,userName From users";
            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            SqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                alicilar.Add(new SelectListItem { Text = oku.GetString(1), Value = oku.GetInt32(0).ToString(), Selected = false });
            }
            ViewBag.alici = new SelectList(alicilar.ToList(), "Value", "Text");
            cnn.Close();
            return View();
        }

        [AutFilter]
        [HttpPost]
        public ActionResult Message(MesajModel model)
        {
            cnn.Open();
            string sorgu = "insert into Messages (Title,Message,SendID,GetID,Tarih,Okundu) Values (@Title,@Message,@SendID,@GetID,@Tarih,@Okundu)";
            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            cmd.Parameters.AddWithValue("@Title", model.baslik);
            cmd.Parameters.AddWithValue("@Message", model.icerik);
            cmd.Parameters.AddWithValue("@SendID", Session["id"].ToString());
            cmd.Parameters.AddWithValue("@GetID", model.mesajGelen);
            cmd.Parameters.AddWithValue("@Tarih", DateTime.Now);
            cmd.Parameters.AddWithValue("@Okundu", 0);
            cmd.ExecuteNonQuery();
            cnn.Close();
            return RedirectToAction("Message");
        }
        List<MesajModel> a = new List<MesajModel>();
        [AutFilter]
        public ActionResult Inbox()
        {
            a.Clear();
            cnn.Open();
            string sorgu = "Select * From Messages m JOIN users u ON m.SendID = u.id Where GetID=@getid  ORDER BY Tarih DESC";
            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            cmd.Parameters.AddWithValue("@getid", Session["id"]);
            SqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                var veri = new MesajModel();
                if (oku["Okundu"].ToString() == "1")
                {
                    veri.Okundumu = "Okundu";
                }
                else
                {
                    veri.Okundumu = "Okunmadı !";
                }
                veri.Kime = oku["userName"].ToString();
                veri.id = oku["id"].ToString();
                veri.baslik = oku["Title"].ToString();
                veri.icerik = oku["Message"].ToString();
                veri.mesajGelen = Convert.ToInt32(oku["GetID"].ToString());
                veri.tarih = DateTime.Parse(oku["Tarih"].ToString());
                a.Add(veri);
            }

            cnn.Close();
            return View(a);

        }
        private void oku(int v)
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand("Update Messages Set Okundu=@Okundu Where id=@id", cnn);
            cmd.Parameters.AddWithValue("@Okundu", 1);
            cmd.Parameters.AddWithValue("@id", v);
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        [AutFilter]
        public ActionResult BtnView(int? id)
        {
            cnn.Open();
            string sorgu = "Select * From Messages m JOIN users u ON m.SendID=u.id Where m.id=@id";
            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Session["okundu"] = dr["Okundu"].ToString();
            Session["CevapID"] = Convert.ToInt32(dr["GetID"]);
            var veri = new MesajModel();
            veri.Kime = dr["userName"].ToString();
            veri.id = dr["id"].ToString();
            veri.baslik = dr["Title"].ToString();
            veri.icerik = dr["Message"].ToString();
            veri.Kim = dr["userName"].ToString();
            veri.mesajGelen = Convert.ToInt32(dr["SendID"].ToString());
            veri.tarih = DateTime.Parse(dr["Tarih"].ToString());
            cnn.Close();
            return View(veri);
        }

        [HttpPost]
        public ActionResult BtnView(MesajModel model)
        {
            int x=Convert.ToInt32(model.id);
            oku(x);
            cnn.Open();
            if (!model.baslik.StartsWith("RE-"))
            {
                model.baslik= "RE-" + model.baslik;
            }

            string sorgu = "insert into Messages (Title,Message,SendID,GetID,Tarih,Okundu) Values (@Title,@Message,@SendID,@GetID,@Tarih,@Okundu)";
            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            cmd.Parameters.AddWithValue("@Title", model.baslik);
            cmd.Parameters.AddWithValue("@Message", model.Cevap);
            cmd.Parameters.AddWithValue("@SendID", Session["id"].ToString());
            cmd.Parameters.AddWithValue("@GetID", model.mesajGelen);
            cmd.Parameters.AddWithValue("@Tarih", DateTime.Now);
            cmd.Parameters.AddWithValue("@Okundu", 0);
            cmd.ExecuteNonQuery();
            cnn.Close();
            return RedirectToAction("Inbox");
        }

        public ActionResult Okundu(int? id)
        {
            int x = Convert.ToInt32(id);
            oku(x);

            return RedirectToAction("Inbox");
        }
        List<DenemeModel> p = new List<DenemeModel>();

        public ActionResult DropdownMesaj()
        {
            p.Clear();
            cnn.Open();
            string sorgu = "Select * From Messages Where GetID=@getid ";
            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            cmd.Parameters.AddWithValue("@getid", 9);
            SqlDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                var veri = new DenemeModel();
                veri.baslik0 = oku["Title"].ToString();
                veri.icerik0 = oku["Message"].ToString();
                p.Add(veri);
            }
            ViewBag.MessageCount = p.Count();
            cnn.Close();
            return View(p);
            
            
        }

        public ActionResult Sil(int? id)
        {
            cnn.Open();
            string sorgu = "delete FROM Messages where id=@k";
            SqlCommand cmd = new SqlCommand(sorgu, cnn);
            cmd.Parameters.AddWithValue("@k", Convert.ToInt32(id));
            cmd.ExecuteNonQuery();
            cnn.Close();
            return RedirectToAction("Inbox");
        }
    }
}