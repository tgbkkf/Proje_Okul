using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BilgeKoleji.Models
{
    public class giris
    {
        [DisplayName("Kullanıcı Adı")]
        public string kulAdi { get; set; }
        [DisplayName("Şifre")]
        [DataType(DataType.Password)]
        public string sifre { get; set; }
    }
}