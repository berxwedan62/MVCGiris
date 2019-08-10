using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCGiris.Models
{
    public class JsonMessage
    {
        public int count { get; set; }
        public string baslik { get; set; }
        public string mesaj { get; set; }
    }
}