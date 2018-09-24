using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class ogrenci_devamsizlik
    {
        public int id { get; set; }
        [ForeignKey("devamsizlik")]
        public int devamsizlik_id { get; set; }
        public virtual devamsizlik devamsizlik { get; set; }
        [ForeignKey("ogrenci")]
        public int ogrenci_id { get; set; }
        public virtual ogrenci ogrenci { get; set; }
        [DisplayName("Tarih")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime tarih { get; set; }
        [ForeignKey("donem")]
        public int? donem_id { get; set; }
        public virtual donem donem { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }
    }
}