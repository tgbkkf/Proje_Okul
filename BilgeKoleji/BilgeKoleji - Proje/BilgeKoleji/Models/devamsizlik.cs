using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class devamsizlik
    {
        public int id { get; set; }
        [DisplayName("Devamsızlık Türü")]
        public string name { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }
        public ICollection<ogrenci_devamsizlik> ogrenciDevamsizliklar { get; set; }
    }
}