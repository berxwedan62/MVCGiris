using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCGiris.Models
{
    public class HaberModel
    {
        public string id { get; set; }
        public string baslik { get; set; }
        [StringLength(500, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 1)]
        public string resim { get; set; }
        public string icerik { get; set; }
        public string kategori { get; set; }
        //public kategori kategoriler { get; set; }
        public string aktif { get; set; }
        public string tarih { get; set; }
    }


}