using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MVCGiris
{
    public class MetinKısalt
    {
        public string baslik(string metin)

        {
            if (metin.Length>40)
            {
                metin = Regex.Replace(metin, @"<(.\n)*?>", string.Empty);

                if (metin.Length > 40) metin = metin.Substring(0, 40);

                metin = metin + "...";
            }
           

            return metin;

        }

        public string icerik(string metin)

        {
            if (metin.Length > 120)
            {
                metin = Regex.Replace(metin, @"<(.\n)*?>", string.Empty);

                if (metin.Length > 120) metin = metin.Substring(0, 120);

                metin = metin + "...";
            }


            return metin;

        }
    }

}