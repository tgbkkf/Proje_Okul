using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class donem
    {
        public int id { get; set; }
        [DisplayName("Dönem İsmi")]
        public string isim { get; set; }
        [DisplayName("Başlangıç Tarihi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime baslangicTarihi { get; set; }
        [DisplayName("Bitiş Tarihi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime bitisTarihi { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }
    }
}