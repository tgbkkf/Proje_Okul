using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class ogretmen_sube
    {
        public int id { get; set; }
        [ForeignKey("ogretmen")]
        public int ogretmen_id { get; set; }
        public virtual ogretmen ogretmen { get; set; }
        [ForeignKey("sube")]
        public int sube_id { get; set; }
        public virtual sube sube { get; set; }
        [ForeignKey("donem")]
        public int? donem_id { get; set; }
        public virtual donem donem { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }
    }
}