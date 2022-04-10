using System;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;


namespace TeletıpWeb.Models
{
    public class TeleProgram
    {
        Auto insta;
        
        public async Task<dynamic> Kostur(int hastanenumara, string nos)
        {
                insta=new Auto(hastanenumara, nos);
            
                int width = 200;
                int height = 40;
                Console.SetWindowSize(width, height);

                Console.WriteLine("Teletıpa giriş yapılıyor...", Console.WindowWidth, Console.WindowHeight);
                insta.Insta("kullanıcıAdı", "şifre");

                string pageSource = insta.driver.PageSource;

                string token = insta.getBetween(pageSource, "var bearerToken = 'Bearer ' + '", "';");

                if (String.IsNullOrEmpty(token))
                {
                    Console.WriteLine("E-mail yada şifre yanlış.");

                }

                //insta = new Auto();

                //insta.OpenTxt();

                insta.HastaneSecim();

                await insta.Search(token);

                await insta.Hatalar();


            return (insta.Dataframe, insta.hstShort);



        }

       //public void Sql()
       //{
       //    insta.SQLQuaries();
       //}
        public void Yazdır(DataFrame data,string kisa)
        {
            
            Auto yeni = new Auto();
            DataFrame datax = yeni.SQLQuaries(data);

            yeni.ExcelYaz(datax, kisa);
        }    

    }
}




//tumhastanelar =['Adana Özel Acıbadem Hastanesi', 'Ankara Acıbadem Hastanesi', 'Aydın Bodrum Klinik International', 'Bursa Özel Acıbadem Hastanesi', 'Eskişehir Özel Acıbadem Hastanesi', 'Istanbul Acıbadem Ataşehir Cerrahi Tıp Merkezi', 'Istanbul Acıbadem Bağdat Cerrahi Tıp Merkezi', 'Istanbul Acıbadem Bahçeşehir Tıp Merkezi', 'Istanbul Acıbadem Beylikdüzü Cerrahi Tıp Mrk.', 'Istanbul Acıbadem Etiler Tıp Merkezi', 'Istanbul Acıbadem Göktürk Tıp Merkezi', 'Istanbul Acıbadem Hastanesi Fulya', 'Istanbul Acıbadem Hastanesi Kozyatağı', 'Istanbul Acıbadem Hastanesi Taksim', 'Istanbul Acıbadem Zekeriyaköy Tıp Merkezi', 'Istanbul Altunizade Özel Acıbadem Hastanesi', 'Istanbul Bakırköy Özel Acıbadem Hastanesi', 'Istanbul International Özel Acıbadem Hastanesi', 'Istanbul Kadıköy Özel Acıbadem Hastanesi', 'Istanbul Maslak Özel Acıbadem Hastanesi', 'Özel Acıbadem Atakent Hastanesi', 'Kayseri Özel Acıbadem Hastanesi', 'Kocaeli Özel Acıbadem Hastanesi', 'Muğla Bodrum Özel Acıbadem Hastanesi']
//   if hastane[0] == tumhastanelar[0]:
//       kisaltma = "ADN"
//   elif hastane[0]==tumhastanelar[1]:
//       kisaltma = "ANK"
//   elif hastane[0]==tumhastanelar[2]:
//       kisaltma = "BDRINT"
//   elif hastane[0]==tumhastanelar[3]:
//       kisaltma = "BRS"
//   elif hastane[0]==tumhastanelar[4]:
//       kisaltma = "ESK"
//   elif hastane[0]==tumhastanelar[5]:
//       kisaltma = "ATASEHIR"
//   elif hastane[0]==tumhastanelar[6]:
//       kisaltma = "BAGDAT"
//   elif hastane[0]==tumhastanelar[7]:
//       kisaltma = "BSEHIR"
//   elif hastane[0]==tumhastanelar[8]:
//       kisaltma = "BEYLIKDUZU"
//   elif hastane[0]==tumhastanelar[9]:
//       kisaltma = "ETILER"
//   elif hastane[0]==tumhastanelar[10]:
//       kisaltma = "KBURGAZ"
//   elif hastane[0]==tumhastanelar[11]:
//       kisaltma = "FLY"
//   elif hastane[0]==tumhastanelar[12]:
//       kisaltma = "KOZ"
//   elif hastane[0]==tumhastanelar[13]:
//       kisaltma = "TAKSIM"
//   elif hastane[0]==tumhastanelar[14]:
//       kisaltma = "ZKOY"
//   elif hastane[0]==tumhastanelar[15]:
//       kisaltma = "ATZ"
//   elif hastane[0]==tumhastanelar[16]:
//       kisaltma = "BKR"
//   elif hastane[0]==tumhastanelar[17]:
//       kisaltma = "INTYSL"
//   elif hastane[0]==tumhastanelar[18]:
//       kisaltma = "KDK"
//   elif hastane[0]==tumhastanelar[19]:
//       kisaltma = "MSL"
//   elif hastane[0]==tumhastanelar[20]:
//       kisaltma = "ATK"
//   elif hastane[0]==tumhastanelar[21]:
//       kisaltma = "KYS"
//   elif hastane[0]==tumhastanelar[22]:
//       kisaltma = "KCL"
//   elif hastane[0]==tumhastanelar[23]:
//       kisaltma = "BDR"