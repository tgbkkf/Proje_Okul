using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class sube
    {
        public int id { get; set; }
        [DisplayName("Şube")]
        public string isim { get; set; }
        [DisplayName("Sınıf")]
        public string sinif { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }

        public virtual ICollection<ogrenci_sube> ogrenciSubeler { get; set; }
        public virtual ICollection<ogretmen_sube> ogretmenSubeler { get; set; }
        public virtual ICollection<sube_ders> subeDersler { get; set; }
    }
}