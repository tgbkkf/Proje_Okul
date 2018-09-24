namespace BilgeKoleji
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class BilgeDb : DbContext
    {
        public BilgeDb()
            : base("name=BilgeDB")
        {
        }
        public DbSet<belge> belgeler  { get; set; }
        public DbSet<ders> dersler { get; set; }
        public DbSet<devamsizlik> devamsizliklar { get; set; }
        public DbSet<duyuru> duyurular { get; set; }
        public DbSet<iliskisiKesilen> iliskisiKesilenler { get; set; }
        public DbSet<kullanici> kullanicilar { get; set; }
        public DbSet<ogrenci> ogrenciler { get; set; }
        public DbSet<ogrenci_belge> ogrenciBelgeler { get; set; }
        public DbSet<ogrenci_devamsizlik> ogrenciDevamsizliklar { get; set; }
        public DbSet<ogrenci_not> ogrenciNotlar { get; set; }
        public DbSet<ogrenci_odev> ogrenciOdevler { get; set; }
        public DbSet<ogrenci_sube> ogrenciSubeler { get; set; }
        public DbSet<onkayit> onKayitlar { get; set; }
        public DbSet<sube> subeler { get; set; }
        public DbSet<sube_ders> subeDersler { get; set; }
        public DbSet<veli> veliler { get; set; }
        public DbSet<ogretmen> ogretmen { get; set; }
        public DbSet<ogretmen_sube> ogretmen_sube { get; set; }
        public DbSet<donem> donemler { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

       

       
    }
}
