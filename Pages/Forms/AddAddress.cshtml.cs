using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TeletıpWeb.Models;
using Microsoft.Data.Analysis;
using System.ComponentModel.DataAnnotations;

namespace TeletıpWeb.Pages.Forms
{
    
    public class AddAddressModel : PageModel
    {
        static DataFrame x;
        static string y;

        public bool Disbale1 { get; set; }
        public bool Disbale2 { get; set; }

        static List<Deneme> carry = new List<Deneme>();



        [BindProperty]
        public string Sıfır { get; set; }
        
        [BindProperty] 
        public string AccNo { get; set; }
        
        [BindProperty]
        public List<Deneme> Test { get; set; }

        List<Deneme> users = new List<Deneme>();

        [BindProperty]
        
        public int Hastaneno { get; set; }

        [ViewData]
        public string Message { get; set; }

        TeleProgram kontrol;

        public ActionResult Arat()
        {
            Disbale1 = true;
            return Page();
        }

        public ActionResult OnPostDecline()
        {
            Disbale1 = false;
            Disbale2 = true;

            return Page();
        }

        public ActionResult OnGet(List<Deneme> temp)
        {
            Disbale2 = true;
            Message = "Numaralar Aranıyor...";
            if (temp.Count==0)
            {
                users.Add(new Deneme("", "", "", "", "", ""));
                Test = users;
            }
            else
            {
                Test = temp;
            }
            return Page();
        }
        public async Task OnPostAsync()
        {

            Arat();
            kontrol = new TeleProgram();

            if (Sıfır != "Excel'e Yaz")
            {
                List<Deneme> users = new List<Deneme>();
                //users.Add(new Deneme("1", "utku", "futbol"));
                ValueTuple<DataFrame, string> valueTuple = await kontrol.Kostur(Hastaneno, AccNo);
               x = valueTuple.Item1;
               y = valueTuple.Item2;


               for (int i = 0; i < valueTuple.Item1.Rows.Count; i++)
               {
                   users.Add(new Deneme((string)valueTuple.Item1[i, 0], (string)valueTuple.Item1[i, 1], (string)valueTuple.Item1[i, 2], (string)valueTuple.Item1[i, 3], (string)valueTuple.Item1[i, 4], (string)valueTuple.Item1[i, 5]));
                    
               }
                carry = users;

                OnGet(users);
            }
            

            if (Sıfır == "Excel'e Yaz")
            {
                kontrol.Yazdır(x,y);
                OnGet(carry);
            }

            Disbale2 = false;

        }

    }

}   
