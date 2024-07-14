using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StokTakip.Models
{
    public partial class StokTakipContext : DbContext
    {
        public StokTakipContext()
        {
        }

        public StokTakipContext(DbContextOptions<StokTakipContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAltmenu> TblAltmenus { get; set; } = null!;
        public virtual DbSet<TblCari> TblCaris { get; set; } = null!;
        public virtual DbSet<TblKategori> TblKategoris { get; set; } = null!;
        public virtual DbSet<TblMenu> TblMenus { get; set; } = null!;
        public virtual DbSet<TblOdemetipi> TblOdemetipis { get; set; } = null!;
        public virtual DbSet<TblSepet> TblSepets { get; set; } = null!;
        public virtual DbSet<TblSipari> TblSiparis { get; set; } = null!;
        public virtual DbSet<TblUrun> TblUruns { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-HLR82UJ\\SQLEXPRESS;Database=StokTakip;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblAltmenu>(entity =>
            {
                entity.ToTable("TBL_ALTMENU");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Altmenuad)
                    .HasMaxLength(100)
                    .HasColumnName("ALTMENUAD");

                entity.Property(e => e.Menuid).HasColumnName("MENUID");
            });

            modelBuilder.Entity<TblCari>(entity =>
            {
                entity.ToTable("TBL_CARI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adres)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("ADRES");

                entity.Property(e => e.Bakiye)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("BAKIYE");

                entity.Property(e => e.Eposta)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EPOSTA");

                entity.Property(e => e.OlusturmaTarihi)
                    .HasColumnType("datetime")
                    .HasColumnName("OLUSTURMA_TARIHI");

                entity.Property(e => e.Telefon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TELEFON");

                entity.Property(e => e.Unvan)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("UNVAN");
            });

            modelBuilder.Entity<TblKategori>(entity =>
            {
                entity.ToTable("TBL_KATEGORI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Kategori)
                    .HasMaxLength(100)
                    .HasColumnName("KATEGORI");
            });

            modelBuilder.Entity<TblMenu>(entity =>
            {
                entity.ToTable("TBL_MENU");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Menuad)
                    .HasMaxLength(10)
                    .HasColumnName("MENUAD")
                    .IsFixedLength();
            });

            modelBuilder.Entity<TblOdemetipi>(entity =>
            {
                entity.ToTable("TBL_ODEMETIPI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Komisyon)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("KOMISYON");

                entity.Property(e => e.OdemeAdi)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ODEME_ADI");
            });

            modelBuilder.Entity<TblSepet>(entity =>
            {
                entity.ToTable("TBL_SEPET");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adet).HasColumnName("ADET");

                entity.Property(e => e.SatisTutar)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("SATIS_TUTAR");
                entity.Property(e => e.ToplamTutar)
                   .HasColumnType("decimal(18, 2)")
                   .HasColumnName("TOPLAM_TUTAR");
                entity.Property(e => e.SiparisId).HasColumnName("SIPARIS_ID");
                entity.Property(e => e.CariId).HasColumnName("CARI_ID");
                entity.Property(e => e.UrunId).HasColumnName("URUN_ID");
            });

            modelBuilder.Entity<TblSipari>(entity =>
            {
                entity.ToTable("TBL_SIPARIS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Aciklama)
                    .HasMaxLength(500)
                    .HasColumnName("ACIKLAMA");

                entity.Property(e => e.AlinanTutar)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ALINAN_TUTAR");

                entity.Property(e => e.CariId).HasColumnName("CARI_ID");

                entity.Property(e => e.KayitTarihi)
                    .HasColumnType("datetime")
                    .HasColumnName("KAYIT_TARIHI");

                entity.Property(e => e.OdemeSeklİ).HasColumnName("ODEME_SEKLİ");
            });

            modelBuilder.Entity<TblUrun>(entity =>
            {
                entity.ToTable("TBL_URUN");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Fiyat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("FIYAT");

                entity.Property(e => e.Kategoriid).HasColumnName("KATEGORIID");

                entity.Property(e => e.Stok).HasColumnName("STOK");

                entity.Property(e => e.StokKodu)
                    .HasMaxLength(10)
                    .HasColumnName("STOK_KODU")
                    .IsFixedLength();

                entity.Property(e => e.Urunadi)
                    .HasMaxLength(500)
                    .HasColumnName("URUNADI");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
