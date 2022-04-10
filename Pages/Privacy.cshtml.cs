using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyWebApplication.Pages
{
    public class DisabledButtonModel : PageModel
    {
        public bool Disbale1 { get; set; }
        public bool Disbale2 { get; set; }

        public void OnGet()
        {

        }

        

        public ActionResult OnPostDecline()
        {
            Disbale1 = false;
            Disbale2 = true;

            return Page();
        }
        public ActionResult OnPostSpprove()
        {
            Disbale1 = true;
            Disbale2 = false;

            return Page();
        }
    }
}