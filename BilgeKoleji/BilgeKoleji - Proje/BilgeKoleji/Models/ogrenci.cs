using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class ogrenci
    {

        public ogrenci()
        {
            ogrenciBelgeler = new HashSet<ogrenci_belge>();
            ogrenciSubeler = new HashSet<ogrenci_sube>();
            ogrenciNotlar = new HashSet<ogrenci_not>();
            ogrenciDevamsizliklar = new HashSet<ogrenci_devamsizlik>();
            ogrenciOdevler = new HashSet<ogrenci_odev>();
        }


        public int id { get; set; }
        [DisplayName("Ad Soyad")]
        public string adSoyad { get; set; }
        [DisplayName("Okul Numarası")]
        public string okulNo { get; set; }
        [DisplayName("Cinsiyet")]
        public bool cinsiyet { get; set; }
        [ForeignKey("veli")]
        public int veli_id { get; set; }
        public virtual veli veli { get; set; }
        [DisplayName("Bitirilen Okul")]
        public string bitirdigiOkul { get; set; }
        [DisplayName("Devam Durumu")]
        [DefaultValue(true)]
        public bool devamDurumu { get; set; }
        [DisplayName("Önkayıt")]
        [DefaultValue(false)]
        public bool onKayitMi { get; set; }
        [DisplayName("Not Ortalaması")]
        public double NotOrtalamasi { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }

        public virtual ICollection<ogrenci_belge> ogrenciBelgeler { get; set; }
        public virtual ICollection<ogrenci_devamsizlik> ogrenciDevamsizliklar { get; set; }
        public virtual ICollection<ogrenci_not> ogrenciNotlar { get; set; }
        public virtual ICollection<ogrenci_odev> ogrenciOdevler { get; set; }
        public virtual ICollection<ogrenci_sube> ogrenciSubeler { get; set; }

    }
}