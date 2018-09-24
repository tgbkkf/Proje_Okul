using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class ogrenci_not
    {
        
        public int id { get; set; }
        [DisplayName("Not")]
        public int not { get; set; }
        [ForeignKey("ders")]
        public int ders_id { get; set; }
        public virtual ders ders { get; set; }
        [ForeignKey("ogrenci")]
        public int ogrenci_id { get; set; }
        public virtual ogrenci ogrenci { get; set; }
        [ForeignKey("donem")]
        public int? donem_id { get; set; }
        public virtual donem donem { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }
    }
}