using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCGiris.Models
{
    public class MesajModel
    {
        public string id { get; set; }
        public string baslik { get; set; }
        public string icerik { get; set; }
        public int mesajAtan { get; set; }
        public int mesajGelen { get; set; }
        public string Kim{get;set;}
        public string Kime { get; set; }
        public DateTime tarih { get; set; }
        public string Okundumu { get; set; }
        public string Cevap { get; set; }
        
    }
}