using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeletıpWeb.Models;



namespace TeletıpWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty(SupportsGet =true)] 
        public string City { get; set; }
        public void OnGet()
        {
            if (String.IsNullOrEmpty(City))
            {
                City = "The WEB";
            }

        }
        public void OnPost()
        {
            
        }
    }


}
