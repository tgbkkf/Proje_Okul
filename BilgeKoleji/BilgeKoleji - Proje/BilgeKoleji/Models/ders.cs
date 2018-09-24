using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class ders
    {
        public int id { get; set; }
        [DisplayName("Ders Adı")]
        public string isim { get; set; }
        [DisplayName("Haftalık Ders Saati")]
        public int haftalikSaat { get; set; }
        [ForeignKey("_ders")]
        public int? ders_id { get; set; }
        public virtual ders _ders { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }

        public ICollection<sube_ders> subeDersler { get; set; }
        public ICollection<ders> dersler { get; set; }

    }
}