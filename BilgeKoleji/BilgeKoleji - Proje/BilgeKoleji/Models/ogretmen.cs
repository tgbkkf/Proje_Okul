using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class ogretmen
    {
        public int id { get; set; }
        [DisplayName("Ad Soyad")]
        public string adiSoyadi { get; set; }
        [DisplayName("Branş")]
        public string brans { get; set; }
        [DisplayName("Cinsiyet")]
        public bool cinsiyet { get; set; }
        [DisplayName("Görevi")]
        public string gorev { get; set; }
        [DisplayName("Telefon")]
        public string telefon { get; set; }
        [DisplayName("Cep Telefonu")]
        public string cepTelefonu { get; set; }
        [DisplayName("EPosta")]
        public string ePosta { get; set; }
        [DisplayName("Adres")]
        public string adres { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }

        public virtual ICollection<ogretmen_sube> ogretmenSubeler { get; set; }
    }
}