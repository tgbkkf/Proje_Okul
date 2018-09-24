using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class ogrenci_belge
    {
        public int id { get; set; }
        [DisplayName("Belge")]
        public string belgeYolu { get; set; }
        [ForeignKey("belge")]
        public int belge_id { get; set; }
        public virtual belge belge { get; set; }
        [ForeignKey("ogrenci")]
        public int ogrenci_id { get; set; }
        public virtual ogrenci ogrenci { get; set; }
        [ForeignKey("donem")]
        public int donem_id { get; set; }
        public virtual donem donem { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }
    }
}