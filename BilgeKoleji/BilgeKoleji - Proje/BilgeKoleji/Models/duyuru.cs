using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class duyuru
    {
        public int id { get; set; }
        [DisplayName("Mesaj")]
        public string mesaj { get; set; }
        [DisplayName("Kayıt Tarihi")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime tarihi { get; set; }
        [DisplayName("Aktiflik Durumu")]
        [DefaultValue(true)]
        public bool aktifMi { get; set; }
        [DefaultValue(false)]
        public bool silindiMi { get; set; }
    }
}