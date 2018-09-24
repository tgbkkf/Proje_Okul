using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{

    public class belge
    {
        public int id { get; set; }
        [DisplayName("İsim")]
        public string isim { get; set; }
        [DisplayName("Belge")]
        public string belgeYolu { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }
    }
}