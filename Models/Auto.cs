using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using Microsoft.Data.Analysis;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Threading;

namespace TeletıpWeb.Models
{
    public class Auto
    {

        public DataFrame Dataframe { get; set; }

        private string tempurl;
        public string token;
        public string password;
        public string hstShort;
        public string hstKey;
        public string searchString;

        public string pv1sql;
        public string istemsql;
        public string tektiksql;
        string nos;
        int hastanenuma;

        List<string> orderids = new List<string>();
        StringDataFrameColumn names = new StringDataFrameColumn("Soyad-Ad", 0);
        StringDataFrameColumn istem = new StringDataFrameColumn("İstem Yöntemi", 0);
        StringDataFrameColumn accessionno = new StringDataFrameColumn("Accession Numarası", 0);
        public string[] accnos;
        public IWebDriver driver;

        public Auto()
        {


        }

        public Auto(int aHastanenumara, string aNos)
        {
            nos = aNos;
            hastanenuma = aHastanenumara;
            ChromeOptions options = new ChromeOptions();
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            chromeDriverService.SuppressInitialDiagnosticInformation = true;
            //options.AddArgument("--start-maximized");
            options.AddArgument("--headless");
            // options.AddArgument("window-size=1200x600");
            driver = new ChromeDriver(chromeDriverService, options);

        }

        public class SelectableEnumItem
        {
            public string SearchAccessionNumber { get; set; }
            public string HL7PatientName { get; set; }
            public string HL7Modality { get; set; }


        }

        //public void OpenTxt()
        //{
        //    var p = new Process();
        //    p.StartInfo = new ProcessStartInfo("TeletıpAranacakNumaralar.txt")
        //    {
        //        UseShellExecute = true
        //    };
        //    p.Start();
        //    Console.WriteLine("\r\nArayacağınız accesion numaraları açılan text dosyasına girin ve girdikten sonra text dosyasını kapatın.");
        //    p.WaitForExit();
        //    
        //}


        public string getBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }


        public void Insta(string username, string password)
        {
            //Thread.Sleep(3000);
            bool x = true;
            while (x)
            {
                try
                {
                    driver.Navigate().GoToUrl("https://stm.teletip.saglik.gov.tr/Trace/InstitutionSendDataTrace");
                    x = false;
                }
                catch (WebDriverException ex)
                {

                    Console.WriteLine("Teletıp'a bağlanılamadı, bir daha deneniyor...");
                    driver.Quit();
                    Thread.Sleep(5000);

                }
            }

            IWebElement user = driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/section/form/div[2]/div/input"));
            var pass = driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/section/form/div[3]/div/input"));

            user.SendKeys(username);
            pass.SendKeys(password);

            IWebElement loginbutton = driver.FindElement(By.XPath("//*[@id='moh']/div[2]/div/section/form/div[4]/div/button"));
            loginbutton.Click();
        }


        public void HastaneSecim()
        {

            int zero = 1;
            string[] tumhastanelar = { "Adana Özel Acıbadem Hastanesi", "Ankara Acıbadem Hastanesi", "Aydın Bodrum Klinik International", "Bursa Özel Acıbadem Hastanesi", "Eskişehir Özel Acıbadem Hastanesi", "Istanbul Acıbadem Ataşehir Cerrahi Tıp Merkezi", "Istanbul Acıbadem Bağdat Cerrahi Tıp Merkezi", "Istanbul Acıbadem Bahçeşehir Tıp Merkezi", "Istanbul Acıbadem Beylikdüzü Cerrahi Tıp Mrk.", "Istanbul Acıbadem Etiler Tıp Merkezi", "Istanbul Acıbadem Göktürk Tıp Merkezi", "Istanbul Acıbadem Hastanesi Fulya", "Istanbul Acıbadem Hastanesi Kozyatağı", "Istanbul Acıbadem Hastanesi Taksim", "Istanbul Acıbadem Zekeriyaköy Tıp Merkezi", "Istanbul Altunizade Özel Acıbadem Hastanesi", "Istanbul Bakırköy Özel Acıbadem Hastanesi", "Istanbul International Özel Acıbadem Hastanesi", "Istanbul Kadıköy Özel Acıbadem Hastanesi", "Istanbul Maslak Özel Acıbadem Hastanesi", "Özel Acıbadem Atakent Hastanesi", "Kayseri Özel Acıbadem Hastanesi", "Kocaeli Özel Acıbadem Hastanesi", "Muğla Bodrum Özel Acıbadem Hastanesi" };

            Console.WriteLine("\r\nArama yapacağınız hastaneyi seçin(solundaki numarayı girin ve 'Enter' tuşuna basın");
            foreach (string i in tumhastanelar)
            {
                Console.WriteLine("[" + zero + "]" + " " + i);
                zero++;
            }
            //int hst = Int32.Parse(Console.ReadLine());

            switch (hastanenuma + 1)
            {
                case 1:
                    Console.WriteLine(tumhastanelar[0] + " Seçildi");
                    hstShort = "ADN";
                    hstKey = "1908";

                    break;
                case 2:
                    Console.WriteLine(tumhastanelar[1] + " Seçildi");
                    hstShort = "ANK";
                    hstKey = "2211";
                    break;
                case 3:
                    Console.WriteLine(tumhastanelar[2] + " Seçildi");
                    hstShort = "BDRINT";
                    hstKey = "2214";
                    break;
                case 4:
                    Console.WriteLine(tumhastanelar[3] + " Seçildi");
                    hstShort = "BRS";
                    hstKey = "1911";
                    break;
                case 5:
                    Console.WriteLine(tumhastanelar[4] + " Seçildi");
                    hstShort = "ESK";
                    hstKey = "1904";
                    break;
                case 6:
                    Console.WriteLine(tumhastanelar[5] + " Seçildi");
                    hstShort = "ATASEHIR";
                    hstKey = "2212";
                    break;
                case 7:
                    Console.WriteLine(tumhastanelar[6] + " Seçildi");
                    hstShort = "BAGDAT";
                    hstKey = "2205";
                    break;
                case 8:
                    Console.WriteLine(tumhastanelar[7] + " Seçildi");
                    hstShort = "BSEHIR";
                    hstKey = "2206";
                    break;
                case 9:
                    Console.WriteLine(tumhastanelar[8] + " Seçildi");
                    hstShort = "BEYLIKDUZU";
                    hstKey = "2250";
                    break;
                case 10:
                    Console.WriteLine(tumhastanelar[9] + " Seçildi");
                    hstShort = "ETILER";
                    hstKey = "2207";
                    break;
                case 11:
                    Console.WriteLine(tumhastanelar[10] + " Seçildi");
                    hstShort = "KBURGAZ";
                    hstKey = "2208";
                    break;
                case 12:
                    Console.WriteLine(tumhastanelar[11] + " Seçildi");
                    hstShort = "FLY";
                    hstKey = "2213";
                    break;
                case 13:
                    Console.WriteLine(tumhastanelar[12] + " Seçildi");
                    hstShort = "KOZ";
                    hstKey = "2209";
                    break;
                case 14:
                    Console.WriteLine(tumhastanelar[13] + " Seçildi");
                    hstShort = "TAKSIM";
                    hstKey = "2210";
                    break;
                case 15:
                    Console.WriteLine(tumhastanelar[14] + " Seçildi");
                    hstShort = "ZKOY";
                    hstKey = "2251";
                    break;
                case 16:
                    Console.WriteLine(tumhastanelar[15] + " Seçildi");
                    hstShort = "ATZ";
                    hstKey = "1909";
                    break;
                case 17:
                    Console.WriteLine(tumhastanelar[16] + " Seçildi");
                    hstShort = "BKR";
                    hstKey = "1910";
                    break;
                case 18:
                    Console.WriteLine(tumhastanelar[17] + " Seçildi");
                    hstShort = "INTYSL";
                    hstKey = "1913";
                    break;
                case 19:
                    Console.WriteLine(tumhastanelar[18] + " Seçildi");
                    hstShort = "KDK";
                    hstKey = "1914";
                    break;
                case 20:
                    Console.WriteLine(tumhastanelar[19] + " Seçildi");
                    hstShort = "MSL";
                    hstKey = "1907";
                    break;
                case 21:
                    Console.WriteLine(tumhastanelar[20] + " Seçildi");
                    hstShort = "ATK";
                    hstKey = "1624";
                    break;
                case 22:
                    Console.WriteLine(tumhastanelar[21] + " Seçildi");
                    hstShort = "KYS";
                    hstKey = "1905";
                    break;
                case 23:
                    Console.WriteLine(tumhastanelar[22] + " Seçildi");
                    hstShort = "KCL";
                    hstKey = "1906";
                    break;
                case 24:
                    Console.WriteLine(tumhastanelar[23] + " Seçildi");
                    hstShort = "BDR";
                    hstKey = "1903";
                    break;

            }
        }


        public void URLMaker()
        {
            //accnos = File.ReadAllLines("TeletıpAranacakNumaralar.txt");
            accnos = nos.Replace("\n", "").Split('\r').ToArray();
            accnos = accnos.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            StringBuilder sb = new StringBuilder();

            foreach (string i in accnos)
            {
                //Console.WriteLine(i);

                //string temp0 = i.Replace("\r\n", string.Empty);
                if (i != accnos.Last())
                {

                    string temp = "\"" + i + "\"" + ",";
                    sb.Append(temp);
                }
                else
                {
                    string temp = "\"" + i + "\"";
                    sb.Append(temp);
                }

            }
            tempurl = "https://api.teletip.saglik.gov.tr/Common.WebApi/api/STM/GetInstitutionSendDataControlList?advancedSearch={\"InstitutionId\":" + hstKey + ",\"AccessionNumber\":%5B" + sb + "%5D,\"CitizenId\":%5B%5D}";
            //"https://api.teletip.saglik.gov.tr/Common.WebApi/api/STM/GetInstitutionSendDataControlList?advancedSearch={\"InstitutionId\":1904,\"AccessionNumber\":%5B\"2620456\",\"5590361\",\"5590365\",\"5589976\",\"5589980\",\"5589983\",\"5589979\",\"5589986\",\"5589368\",\"5589372\",\"5597065\",\"5578759\",\"5590052\",\"5590054\"%5D,\"CitizenId\":%5B%5D}"))
        }


        public async Task Search(string aToken)
        {

            URLMaker();
            token = aToken;
            HttpClientHandler handler = new HttpClientHandler();

            // If you are using .NET Core 3.0+ you can replace `~DecompressionMethods.None` to `DecompressionMethods.All`
            handler.AutomaticDecompression = DecompressionMethods.All;

            using HttpClient httpClient = new HttpClient(handler);
            using HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), tempurl);
            request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
            request.Headers.TryAddWithoutValidation("sec-ch-ua", "\" Not A;Brand\";v=\"99\", \"Chromium\";v=\"90\", \"Google Chrome\";v=\"90\"");
            request.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
            request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token);
            request.Headers.TryAddWithoutValidation("sec-ch-ua-mobile", "?0");
            request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36");
            request.Headers.TryAddWithoutValidation("Origin", "https://stm.teletip.saglik.gov.tr");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "same-site");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
            request.Headers.TryAddWithoutValidation("Referer", "https://stm.teletip.saglik.gov.tr/");
            request.Headers.TryAddWithoutValidation("Accept-Language", "tr-TR,tr;q=0.9,en-US;q=0.8,en;q=0.7");

            var response = await httpClient.SendAsync(request);
            string json = await response.Content.ReadAsStringAsync();
            JArray ara = JArray.Parse(json);
            var pick = ara.ToObject<List<SelectableEnumItem>>();




            for (int i = 0; i < ara.Count; i++)
            {
                if (pick[i].HL7PatientName != null)
                {
                    string words = pick[i].HL7PatientName.Replace("^", " "); ;
                    words = words.Trim();

                    names.Append(words);
                }
                else
                {
                    names.Append("null");
                }
                if (pick[i].HL7Modality != null)
                {
                    istem.Append(pick[i].HL7Modality);
                }
                else
                {
                    istem.Append("null");
                }

                accessionno.Append((string)ara[i]["SearchAccessionNumber"]);

                string OrderId = (string)ara[i]["OrderId"];
                orderids.Add(OrderId);
            }
        }




        public async Task Hatalar()
        {
            HttpClientHandler handler1 = new HttpClientHandler();

            using HttpClient httpClient1 = new HttpClient(handler1);

            StringDataFrameColumn NoHata = new StringDataFrameColumn("Hata", 0);
            StringDataFrameColumn Tc_No = new StringDataFrameColumn("TC Kimlik No", 0);
            StringDataFrameColumn Sut_kodu = new StringDataFrameColumn("Sut kodu", 0);

            foreach (string i in orderids)
            {
                using HttpRequestMessage request1 = new HttpRequestMessage(new HttpMethod("GET"), "https://api.teletip.saglik.gov.tr/Common.WebApi/api/STM/GetMedulaStatus?orderId=" + i);

                request1.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                request1.Headers.TryAddWithoutValidation("sec-ch-ua", "^^");
                request1.Headers.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
                request1.Headers.TryAddWithoutValidation("Authorization", "Bearer " + token);
                request1.Headers.TryAddWithoutValidation("sec-ch-ua-mobile", "?0");
                request1.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.85 Safari/537.36");
                request1.Headers.TryAddWithoutValidation("Origin", "https://stm.teletip.saglik.gov.tr");
                request1.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "same-site");
                request1.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
                request1.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
                request1.Headers.TryAddWithoutValidation("Referer", "https://stm.teletip.saglik.gov.tr/");
                request1.Headers.TryAddWithoutValidation("Accept-Language", "tr-TR,tr;q=0.9,en-US;q=0.8,en;q=0.7");

                HttpResponseMessage response1 = await httpClient1.SendAsync(request1);
                string json = await response1.Content.ReadAsStringAsync();
                JObject hata = JObject.Parse(json);
                //string Tc_No = (string)hata["MedulaResponseMessage"];
                //Console.WriteLine(Tc_No);



                if ((string)hata["Error"] == "")
                {
                    NoHata.Append("İşlem başarı ile sona erdi.");
                }
                else if ((string)hata["Error"] == null)
                {
                    NoHata.Append("Accession No yanlış.");
                }
                else
                {
                    NoHata.Append((string)hata["Error"]);
                }

                Tc_No.Append((string)hata["CitizenId"]);
                Sut_kodu.Append((string)hata["SutCode"]);

            }
            DataFrame df = new DataFrame(accessionno, NoHata, names, Tc_No, Sut_kodu, istem);
            Dataframe = df;
            var x = Dataframe.ToString();
            df.PrettyPrint();
        }

        List<string> basarilicheck = new List<string>();
        List<string> pv1check = new List<string>();
        List<string> istemcheck = new List<string>();
        List<string> tektikcheck = new List<string>();

        public DataFrame SQLQuaries(DataFrame data)
        {
            
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            /*
            string temp = "\"" + i + "\"" + ",";
            sb.Append(temp);
            */
            for (int i = 0; i <= data.Rows.Count - 1; i++)
            {
                if ((string)data[i, 1] == "İşlem başarı ile sona erdi.")
                {
                    basarilicheck.Add((string)data[i, 0]);
                }
                else if ((string)data[i, 1] == "PV1-20 alanı Other gönderildiğinden Medulaya bildirim yapılmadı.")
                {
                    pv1check.Add((string)data[i, 0]);

                }
                else if ((string)data[i, 1] == "İstem geldi - Tetkik gelmedi yada eşleşmedi")
                {
                    istemcheck.Add((string)data[i, 0]);
                }
                else if ((string)data[i, 1] == "Tetkik istem nedeni girilmedi ( Radyolojik tetkik istem periyodu listesini kontrol ediniz )")
                {
                    tektikcheck.Add((string)data[i, 0]);
                }
            }

            foreach (string i in pv1check)
            {
                if (i != pv1check.Last())
                {
                    string pv1temp = i + ",";
                    sb.Append(pv1temp);
                }
                else
                {
                    string pv1temp = i + ");";
                    sb.Append(pv1temp);
                }
            }

            foreach (string i in istemcheck)
            {
                if (i != istemcheck.Last())
                {
                    string istemtemp = i + ",";
                    sb1.Append(istemtemp);
                }
                else
                {
                    string istemtemp = i + ");";
                    sb1.Append(istemtemp);
                }
            }

            foreach (string i in tektikcheck)
            {
                if (i != tektikcheck.Last())
                {
                    string tektiktemp = i + ",";
                    sb2.Append(tektiktemp);
                }
                else
                {
                    string tektiktemp = i + ");";
                    sb2.Append(tektiktemp);
                }
            }

            if (sb.Length != 0)
            {
                pv1sql = "update " + hstShort + ".teletip_entegrasyon_orm set is_done = 'F', ORC_1 = 'XO', IS_SGK = 'SGK' where accession_no in (" + sb;
            }

            if (sb1.Length != 0)
            {
                istemsql = "update " + hstShort + ".teletip_entegrasyon_orU set is_SENT='F',ORC_1='XO!$!SC' where accession_no in (" + sb1;
            }

            if (sb2.Length != 0)
            {
                tektiksql = "update " + hstShort + ".teletip_entegrasyon_orm set is_done='F',ORC_1='XO',reason_for_study='TEDAVİYE YANIT DEĞERLENDİRME',REASON_FOR_STUDY_CODE='3' where accession_no in (" + sb2;
            }
            return data;

        }

        public void ExcelYaz(DataFrame datax,string kisa)
        {
            hstShort = kisa;
            // Creating an instance
            // of ExcelPackage
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excel = new ExcelPackage();

            // name of the sheet
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");

            // setting the properties
            // of the work sheet 
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;

            // Setting the properties
            // of the first row
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;

            // Header of the Excel sheet
            workSheet.Cells[1, 1].Value = datax.Columns[0].Name;
            workSheet.Cells[1, 2].Value = datax.Columns[1].Name;
            workSheet.Cells[1, 3].Value = datax.Columns[2].Name;
            workSheet.Cells[1, 4].Value = datax.Columns[3].Name;
            workSheet.Cells[1, 5].Value = datax.Columns[4].Name;
            workSheet.Cells[1, 6].Value = datax.Columns[5].Name;

            workSheet.Cells[1, 7].Value = "PV1-20 alanı Other gönderildiğinden Medulaya bildirim yapılmadı.";
            workSheet.Cells[1, 8].Value = "İstem geldi - Tetkik gelmedi yada eşleşmedi";
            workSheet.Cells[1, 9].Value = "Tetkik istem nedeni girilmedi ( Radyolojik tetkik istem periyodu listesini kontrol ediniz )";

            // Inserting the article data into excel
            // sheet by using the for each loop
            // As we have values to the first row 
            // we will start with second row


            for (int i = 0; i <= datax.Rows.Count - 1; i++)
            {
                workSheet.Cells[i + 2, 1].Value = datax[i, 0];
                workSheet.Cells[i + 2, 2].Value = datax[i, 1];
                workSheet.Cells[i + 2, 3].Value = datax[i, 2];
                workSheet.Cells[i + 2, 4].Value = datax[i, 3];
                workSheet.Cells[i + 2, 5].Value = datax[i, 4];
                workSheet.Cells[i + 2, 6].Value = datax[i, 5];
            }


            workSheet.Cells[2, 7].Value = pv1sql;
            workSheet.Cells[2, 8].Value = istemsql;
            workSheet.Cells[2, 9].Value = tektiksql;


            // By default, the column width is not 
            // set to auto fit for the content
            // of the range, so we are using
            // AutoFit() method here. 
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            workSheet.Column(5).AutoFit();
            workSheet.Column(6).AutoFit();

            // file name with .xlsx extension 
            DateTime now = DateTime.Now;
            string nowStr = now.ToString().Replace(":", ".");

            string strPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            string p_strPath = strPath + "\\" + hstShort + " " + nowStr + ".xlsx";


            if (File.Exists(p_strPath))
                File.Delete(p_strPath);

            // Create excel file on physical disk 

            FileStream objFileStrm = File.Create(p_strPath);
            objFileStrm.Close();

            // Write content to excel file 
            File.WriteAllBytes(p_strPath, excel.GetAsByteArray());
            //Close Excel package
            excel.Dispose();

        }



    }
}
