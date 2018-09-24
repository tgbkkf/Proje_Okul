using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class iliskisiKesilen
    {
        public int id { get; set; }
        [DisplayName("Ad Soyad")]
        public string adSoyad { get; set; }
        [DisplayName("Okul No")]
        public string okulNo { get; set; }
        [DisplayName("Cinsiyet")]
        public bool cinsiyet { get; set; }
        [DisplayName("Bitirilen Okul")]
        public string bitirdigiOkul { get; set; }
        [DisplayName("Not Ortalaması")]
        public decimal notOrtalamsi { get; set; }
        [ForeignKey("veli")]
        public int veli_id { get; set; }
        public virtual veli veli { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }
    }
}