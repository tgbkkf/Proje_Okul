using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class veli
    {
        public int id { get; set; }
        [DisplayName("Veli Ad Soyad")]
        public string adSoyad { get; set; }
        [DisplayName("TC Kimlik No")]
        public string tcKimlik { get; set; }
        [DisplayName("Ev Telefonu")]
        public string evTelefonu { get; set; }
        [DisplayName("İş Telefonu")]
        public string isTelefonu { get; set; }
        [DisplayName("Adresi")]
        public string adres { get; set; }
        [DisplayName("İl/İlçe")]
        public string ilIlce { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }
        public virtual ICollection<ogrenci> ogrenciler { get; set; }
    }
}