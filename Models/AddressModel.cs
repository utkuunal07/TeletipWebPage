using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeletıpWeb.Models
{
    public class AddressModel
    {
        [Required(ErrorMessage ="Accesion No girmelisiniz")]
        public string AccNo { get; set; }
            
    }
    public class UserModel
    {
        //public Input Inputlar { get; set; }
        public List<Deneme> Table { get; set; }

    }
}
