using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class kullanici
    {
        public int id { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string kulAdi { get; set; }
        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [ForeignKey("ogretmen")]
        public int? ogretmen_id { get; set; }
        public virtual ogretmen ogretmen { get; set; }
        [ForeignKey("veli")]
        public int? veli_id { get; set; }
        public virtual veli veli { get; set; }
        [ForeignKey("ogrenci")]
        public int? ogrenci_id { get; set; }
        public virtual ogrenci ogrenci { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }

    }
}