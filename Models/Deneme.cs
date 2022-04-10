using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeletıpWeb.Models
{
    public class Deneme
    {
        public string AccessionNumarası { get; set; }
        public string Hata { get; set; }
        public string SoyadAd { get; set; }
        public string TCNo { get; set; }
        public string Sutkodu { get; set; }
        public string İstemYöntemi { get; set; }

        public Deneme(string accessionNumarası, string hata, string soyadAd, string tCNo, string sutkodu, string istemYöntemi)
        {
            AccessionNumarası = accessionNumarası;
            Hata = hata;
            SoyadAd = soyadAd;
            TCNo = tCNo;
            Sutkodu = sutkodu;
            İstemYöntemi = istemYöntemi;
        }
    }
}
